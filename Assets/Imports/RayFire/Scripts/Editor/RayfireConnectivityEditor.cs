using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace RayFire
{
    [CanEditMultipleObjects]
    [CustomEditor (typeof(RayfireConnectivity))]
    public class RayfireConnectivityEditor : Editor
    {
        RayfireConnectivity conn;
        static Color        wireColor   = new Color (0.58f, 0.77f, 1f);
        static Color        stressColor = Color.green;
        
        // Draw gizmo
        [DrawGizmo (GizmoType.Selected | GizmoType.NonSelected | GizmoType.Pickable)]
        static void DrawGizmosSelected (RayfireConnectivity targ, GizmoType gizmoType)
        {
            // Draw bounding gizmo
            GizmoDraw (targ);
            
            // Has no shards
            if (targ.cluster == null)
                return;
            
            /*
            // Missing shards
            // if (targ.cluster.shards.Count > 0)
            {
                if (RFCluster.IntegrityCheck (targ.cluster) == false)
                    Debug.Log ("RayFire Connectivity: " + targ.name + " has missing shards. Reset or Setup cluster.", targ.gameObject);
                else
                    targ.integrityCheck = false;
            }
            */
            
            // Draw for MeshRoot and RigidRoot in runtime
            if (Application.isPlaying == true || targ.meshRootHost != null)
            {
                ClusterDraw (targ.cluster, targ.showNodes, targ.showConnections);
            }

            // Draw for RigidRoot because Connectivity do not store same shard list
            else if (targ.rigidRootHost != null)
                ClusterDraw (targ.rigidRootHost.cluster, targ.showNodes, targ.showConnections);

            // Draw stresses connections
            StressDraw (targ);
        }

        static void GizmoDraw (RayfireConnectivity targ)
        {
            if (targ.showGizmo == true)
            {
                // Gizmo properties
                Gizmos.color = wireColor;
                if (targ.transform.childCount > 0)
                {
                    Bounds bound = RFCluster.GetChildrenBound (targ.transform);
                    Gizmos.DrawWireCube (bound.center, bound.size);
                }
            }
        }
        
        // Inspector
        public override void OnInspectorGUI()
        {
            // Get target
            conn = target as RayfireConnectivity;
            if (conn == null)
                return;
            
            /*
            int UnyShards = 0;
            for (int i = 0; i < conn.cluster.shards.Count; i++)
                if (conn.cluster.shards[i].uny == true)
                    UnyShards++;
            GUILayout.Label ("  UnyShards " + UnyShards, EditorStyles.boldLabel);   
            */
            
            GUILayout.Space (8);

            ClusterPreviewUI();

            ClusterCollapseUI();

            GUILayout.Space (3);

            
            if (conn.cluster.shards.Count > 0)
            {
                GUILayout.Label ("    Cluster Shards: " + conn.cluster.shards.Count + "/" + conn.initShardAmount);
                GUILayout.Label ("    Amount Integrity: " + conn.AmountIntegrity + "%");
            }
            
            // if (GUILayout.Button("Delete Selected Connection")) DeleteSelectedConnection();

            DrawDefaultInspector();
        }
        
        void ClusterCollapseUI()
        {
            GUILayout.Label ("  Collapse", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();

            GUILayout.Label ("By Area:", GUILayout.Width (55));

            // Start check for slider change
            EditorGUI.BeginChangeCheck();
            conn.cluster.areaCollapse = EditorGUILayout.Slider(conn.cluster.areaCollapse, conn.cluster.minimumArea, conn.cluster.maximumArea);
            if (EditorGUI.EndChangeCheck() == true)
                if (Application.isPlaying)
                    RFCollapse.AreaCollapse (conn, conn.cluster.areaCollapse);

            EditorGUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.Label ("By Size:", GUILayout.Width (55));

            // Start check for slider change
            EditorGUI.BeginChangeCheck();
            conn.cluster.sizeCollapse = EditorGUILayout.Slider(conn.cluster.sizeCollapse, conn.cluster.minimumSize, conn.cluster.maximumSize);
            if (EditorGUI.EndChangeCheck() == true)
                if (Application.isPlaying)
                    RFCollapse.SizeCollapse (conn, conn.cluster.sizeCollapse);

            EditorGUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.Label ("Random:", GUILayout.Width (55));

            // Start check for slider change
            EditorGUI.BeginChangeCheck();
            conn.cluster.randomCollapse = EditorGUILayout.IntSlider(conn.cluster.randomCollapse, 0, 100);
            if (EditorGUI.EndChangeCheck() == true)
                if (Application.isPlaying)
                    RFCollapse.RandomCollapse (conn, conn.cluster.randomCollapse, conn.seed);
            
            EditorGUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();

            if (Application.isPlaying)
            {
                // Start/Stop collapse
                if (conn.collapse.inProgress == false)
                {
                    if (GUILayout.Button ("Start Collapse", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireConnectivity != null)
                                RFCollapse.StartCollapse (targ as RayfireConnectivity);
                }
                else
                {
                    if (GUILayout.Button ("Stop Collapse", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireConnectivity != null)
                                RFCollapse.StopCollapse (targ as RayfireConnectivity);
                }

                // Start/Stop Stress
                if (conn.stress.inProgress == false)
                {
                    if (GUILayout.Button ("Start Stress ", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireConnectivity != null)
                                RFStress.StartStress (targ as RayfireConnectivity);
                }
                else
                {
                    if (GUILayout.Button ("Stop Stress", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireConnectivity != null)
                                RFStress.StopStress (targ as RayfireConnectivity);
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        void ClusterPreviewUI()
        {
            GUILayout.Label ("  Preview", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            
            conn.showGizmo = GUILayout.Toggle (conn.showGizmo, " Show Gizmo ", "Button", GUILayout.Height (22));
            
            GUILayout.BeginHorizontal();

            // Show nodes
            conn.showConnections = GUILayout.Toggle (conn.showConnections, "Show Connections",    "Button", GUILayout.Height (22));
            conn.showNodes       = GUILayout.Toggle (conn.showNodes,       "Show Nodes", "Button", GUILayout.Height (22));
            conn.showStress      = GUILayout.Toggle (conn.showStress,      "Show Stress","Button", GUILayout.Height (22));
            
            if (EditorGUI.EndChangeCheck())
            { 
                foreach (var targ in targets)
                    if (targ as RayfireConnectivity != null)
                    {
                        (targ as RayfireConnectivity).showConnections = conn.showConnections;
                        (targ as RayfireConnectivity).showNodes = conn.showNodes;
                        SetDirty (targ as RayfireConnectivity);
                    }
                SceneView.RepaintAll();
            }

            EditorGUILayout.EndHorizontal();
        }
        
        
        // Draw Connections and Nodes by act and uny states
        static void ClusterDraw(RFCluster cluster, bool showNodes, bool showConnections)
        {
            if (showNodes == true || showConnections == true)
            {
                if (cluster != null && cluster.shards.Count > 0)
                {
                    for (int i = 0; i < cluster.shards.Count; i++)
                    {
                        if (cluster.shards[i].tm != null)
                        {
                            // Color
                            if (cluster.shards[i].rigid == null)
                                SetColor (cluster.shards[i].uny, cluster.shards[i].act);
                            else 
                            {
                                if (cluster.shards[i].rigid.objectType == ObjectType.Mesh)
                                    SetColor (cluster.shards[i].rigid.activation.unyielding, cluster.shards[i].rigid.activation.activatable);
                                else if (cluster.shards[i].rigid.objectType == ObjectType.MeshRoot)
                                    SetColor (cluster.shards[i].uny, cluster.shards[i].act);
                            }

                            // Nodes
                            if (showNodes == true)
                                Gizmos.DrawWireSphere (cluster.shards[i].tm.position, cluster.shards[i].sz / 12f);
                            
                            // Connection
                            if (showConnections == true)
                            {
                                // Debug.Log (cluster.shards[i].nIds.Count);
                                
                                // Has no neibs
                                if (cluster.shards[i].nIds.Count == 0)
                                    continue;
                                
                                // Shard has neibs but neib shards not initialized by nIds
                                if (cluster.shards[i].neibShards == null)
                                    cluster.shards[i].neibShards = new List<RFShard>();
                                
                                // Reinit
                                if (cluster.shards[i].neibShards.Count == 0)
                                    for (int n = 0; n < cluster.shards[i].nIds.Count; n++)
                                        cluster.shards[i].neibShards.Add (cluster.shards[cluster.shards[i].nIds[n]]);
                                
                                // Preview
                                for (int j = 0; j < cluster.shards[i].neibShards.Count; j++)
                                    if (cluster.shards[i].neibShards[j].tm != null)
                                    {
                                        Gizmos.DrawLine (cluster.shards[i].tm.position, 
                                            (cluster.shards[i].neibShards[j].tm.position - cluster.shards[i].tm.position) / 2f + cluster.shards[i].tm.position);
                                    }
                            }
                        }
                    }
                }
            }
        }
        
        // Draw Connections and Nodes by act and uny states
        static void ClusterDraw(RayfireConnectivity targ)
        {
            if (targ.showNodes == true || targ.showConnections == true)
            {
                if (targ.cluster != null && targ.cluster.shards.Count > 0)
                {
                    for (int i = 0; i < targ.cluster.shards.Count; i++)
                    {
                        if (targ.cluster.shards[i].tm != null)
                        {
                            // Color
                            if (targ.cluster.shards[i].rigid == null)
                                SetColor (targ.cluster.shards[i].uny, targ.cluster.shards[i].act);
                            else 
                            {
                                if (targ.cluster.shards[i].rigid.objectType == ObjectType.Mesh)
                                    SetColor (targ.cluster.shards[i].rigid.activation.unyielding, targ.cluster.shards[i].rigid.activation.activatable);
                                else if (targ.cluster.shards[i].rigid.objectType == ObjectType.MeshRoot)
                                    SetColor (targ.cluster.shards[i].uny, targ.cluster.shards[i].act);
                            }

                            // Nodes
                            if (targ.showNodes == true)
                                Gizmos.DrawWireSphere (targ.cluster.shards[i].tm.position, targ.cluster.shards[i].sz / 12f);
                            
                            // Connection
                            if (targ.showConnections == true)
                            {
                                // has no neibs
                                if (targ.cluster.shards[i].nIds.Count == 0)
                                    continue;
                                
                                // Shard has neibs but neib shards not initialized by nIds
                                if (targ.cluster.shards[i].neibShards == null)
                                    targ.cluster.shards[i].neibShards = new List<RFShard>();
                                
                                // Reinit
                                if (targ.cluster.shards[i].neibShards.Count == 0)
                                    for (int n = 0; n < targ.cluster.shards[i].nIds.Count; n++)
                                        targ.cluster.shards[i].neibShards.Add (targ.cluster.shards[targ.cluster.shards[i].nIds[n]]);
                                
                                // Preview
                                for (int j = 0; j < targ.cluster.shards[i].neibShards.Count; j++)
                                    if (targ.cluster.shards[i].neibShards[j].tm != null)
                                    {
                                        Gizmos.DrawLine (targ.cluster.shards[i].tm.position, 
                                            (targ.cluster.shards[i].neibShards[j].tm.position - targ.cluster.shards[i].tm.position) / 2f + targ.cluster.shards[i].tm.position);
                                    }
                            }
                        }
                    }
                }
            }
        }
        
        // Draw stressed connections
        static void StressDraw (RayfireConnectivity targ)
        {
            if (targ.showStress == true && targ.stress != null && targ.stress.inProgress == true)
            {
                if (targ.cluster != null && targ.cluster.shards.Count > 0)
                {
                    Vector3 pos;
                    for (int i = 0; i < targ.cluster.shards.Count; i++)
                    {
                        if (targ.cluster.shards[i].tm != null)
                        {
                            // Show Path stress
                            /*
                            if (false)
                                if (targ.stress.bySize == true)
                                {
                                    Gizmos.color = ColorByValue (stressColor, targ.cluster.shards[i].sSt, 1f);
                                    Gizmos.DrawWireSphere (targ.cluster.shards[i].tm.position, targ.cluster.shards[i].sz / 12f);
                                }
                            */
                            
                            if (targ.cluster.shards[i].StressState == true)
                            {
                                for (int n = 0; n < targ.cluster.shards[i].nSt.Count / 3; n++)
                                {
                                    if (targ.cluster.shards[i].uny == true)
                                    {
                                        Gizmos.color = Color.yellow;
                                    }
                                    else
                                    {
                                        Gizmos.color = targ.cluster.shards[i].sIds.Count > 0 
                                            ? Color.yellow 
                                            : ColorByValue (stressColor, targ.cluster.shards[i].nSt[n * 3], targ.stress.threshold);
                                    }
                                    
                                    pos = (targ.cluster.shards[i].neibShards[n].tm.position - targ.cluster.shards[i].tm.position) / 2.5f + targ.cluster.shards[i].tm.position;
                                    Gizmos.DrawLine (targ.cluster.shards[i].tm.position, pos);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Set gizmo color by uny and act states
        static void SetColor (bool uny, bool act)
        {
            if (uny == false)
                Gizmos.color = Color.green;
            else
                Gizmos.color = act == true ? Color.magenta : Color.red;
        }
        
        // Color by value
        static Color ColorByValue(Color color, float val, float threshold)
        {
            val     /= threshold;
            color.g =  1f - val;
            color.r =  val;
            return color;
        }
        
        // Set dirty
        void SetDirty (RayfireConnectivity scr)
        {
            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty (scr);
                EditorSceneManager.MarkSceneDirty (scr.gameObject.scene);
            }
        }
        
        /*
        
        /// /////////////////////////////////////////////////////////
        /// Handle selection
        /// /////////////////////////////////////////////////////////
        
        static         Vector2Int currentShardConnection;
		private static int        s_ButtonHash = "ConnectionHandle".GetHashCode();
        
        void OnSceneGUI()
		{
            var targ = conn;
			if (targ == null)
				return;
            
			if (targ.showConnections == true)
			{
				if (targ.cluster != null && targ.cluster.shards.Count > 0)
				{
					int count = targ.cluster.shards.Count;
					for (int i = 0; i < count; i++)
					{
						if (targ.cluster.shards[i].tm != null)
						{
							if (targ.cluster.shards[i].nIds.Count == 0)
								continue;

							if (targ.cluster.shards[i].neibShards != null && targ.cluster.shards[i].neibShards.Count != 0)
							{
								int nCount = targ.cluster.shards[i].neibShards.Count;
								for (int j = 0; j < nCount; j++)
								{
									if (targ.cluster.shards[i].neibShards[j].tm != null)
									{
										Vector3 start = targ.cluster.shards[i].tm.position;
										Vector3 end = start + (targ.cluster.shards[i].neibShards[j].tm.position - start) * 0.5f;
										HandleClick(start, end, targ.cluster.shards[i].id, targ.cluster.shards[i].neibShards[j].id);
                                        
                                        
									}
								}
							}
						}
					}
				}
			}
		}
        
		private static void HandleClick(Vector3 start, Vector3 end, int id1, int id2)
		{
			int id = GUIUtility.GetControlID(s_ButtonHash, FocusType.Passive);
			Event evt = Event.current;

			switch (evt.GetTypeForControl(id))
			{
				case EventType.Layout:
				{
					HandleUtility.AddControl(id, HandleUtility.DistanceToLine(start, end));
					break;
				}
                case EventType.MouseMove:
                {
                    if (id == HandleUtility.nearestControl)
                        HandleUtility.Repaint();
                    break;
                }
                case EventType.MouseDown:
				{
					if (HandleUtility.nearestControl == id && evt.button == 0)
					{
						GUIUtility.hotControl = id; // Grab mouse focus
						HandleClickSelection(evt, id1, id2);
						evt.Use();
					}
					break;
				}
			}
		}

		public static void HandleClickSelection(Event evt, int id1, int id2)
		{
			currentShardConnection.x = id1;
			currentShardConnection.y = id2;
            
            
		}
        
        private void DeleteSelectedConnection()
        {
            var targ = conn;
            if (targ.showConnections == true)
            {
                if (targ.cluster != null && targ.cluster.shards.Count > 0)
                {
                    int count = targ.cluster.shards.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (targ.cluster.shards[i].tm != null)
                        {
                            if (targ.cluster.shards[i].nIds.Count == 0)
                                continue;

                            if (targ.cluster.shards[i].neibShards != null && targ.cluster.shards[i].neibShards.Count != 0)
                            {
                                int nCount = targ.cluster.shards[i].neibShards.Count - 1;
                                for (int j = nCount; j >= 0; --j)
                                {
                                    if (targ.cluster.shards[i].neibShards[j].tm != null)
                                    {
                                        var id  = targ.cluster.shards[i].id;
                                        var nId = targ.cluster.shards[i].neibShards[j].id;
                                        if (currentShardConnection.x == id && currentShardConnection.y == nId || currentShardConnection.y == id && currentShardConnection.x == nId)
                                            targ.cluster.shards[i].RemoveNeibAt(j);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        */
        
    }
}


/*
 if (targ.cluster.shards[i].uny == true)
    {
        Gizmos.color = Color.yellow;
    }
    else
    {
        //if (targ.cluster.shards[i].sIds.Count > 0)
        //{
            if (targ.cluster.shards[i].neibShards[n].sIds.Contains (targ.cluster.shards[i].id) == true || targ.cluster.shards[i].sIds.Contains (targ.cluster.shards[i].neibShards[n].id) == true)
            {
                Gizmos.color = Color.yellow;
            }
        //}
            else
                Gizmos.color     = ColorByValue (stressColor, targ.cluster.shards[i].nStr[n * 3], targ.stress.threshold);
    }




                                    if (targ.cluster.shards[i].uny == true || targ.cluster.shards[i].sIds.Count > 0)
                                        Gizmos.color = Color.yellow;
                                    else
                                        Gizmos.color = ColorByValue (stressColor, targ.cluster.shards[i].nStr[n*3], targ.stress.threshold);

*/