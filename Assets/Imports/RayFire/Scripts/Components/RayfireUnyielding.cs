using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RayFire
{
    [AddComponentMenu ("RayFire/Rayfire Unyielding")]
    [HelpURL ("https://rayfirestudios.com/unity-online-help/components/unity-unyielding-component/")]
    public class RayfireUnyielding : MonoBehaviour
    {
        // Sim Type
        public enum UnySimType
        {

            Original  = 1,
            Inactive  = 2, 
            Kinematic = 3
        }
        
        
        [Header ("  Properties")]
        [Space (3)]
        
        [Tooltip ("Set Unyielding property for children Rigids and Shards.")]
        public bool unyielding = true;
        [Space (2)]
        
        [Tooltip ("Set Activatable property for children Rigids and Shards.")]
        public bool activatable = false;
        
        [Space (2)]
        [Tooltip ("Custom simulation type")]
        public UnySimType simulationType = UnySimType.Original;
        
        [Header ("  Gizmo")]
        [Space (3)]
        
        [Tooltip ("Unyielding gizmo center.")]
        public Vector3 centerPosition;
        [Space (2)]
        
        [Tooltip ("Unyielding gizmo size.")]
        public Vector3 size = new Vector3(1f,1f,1f);
        
        // Hidden
        [HideInInspector] public RayfireRigid        rigidHost;
        [HideInInspector] public List<RayfireRigid>  rigidList;
        [HideInInspector] public List<RFShard>       shardList;
        [HideInInspector] public bool                showGizmo = true;
        [HideInInspector] public bool                showCenter;
        [HideInInspector] public int                 id;
        
        /// /////////////////////////////////////////////////////////
        /// Connected Cluster setup
        /// /////////////////////////////////////////////////////////
        
        // Set clusterized rigids uny state and mesh root rigids
        public static void ClusterSetup (RayfireRigid rigid)
        {
            if (rigid.simulationType == SimType.Inactive || rigid.simulationType == SimType.Kinematic)
            {
                RayfireUnyielding[] unyArray =  rigid.GetComponents<RayfireUnyielding>();
                for (int i = 0; i < unyArray.Length; i++)
                    if (unyArray[i].enabled == true)
                    {
                        unyArray[i].rigidHost = rigid;
                        ClusterOverlap (unyArray[i]);
                    }
            }
        }
        
        // Set uny state for mesh root rigids. Used by Mesh Root. Can be used for cluster shards
        static void ClusterOverlap (RayfireUnyielding uny)
        {
            // Get target mask and overlap colliders
            int               finalMask     = ClusterLayerMask(uny.rigidHost);
            Collider[]        colliders     = Physics.OverlapBox (uny.transform.TransformPoint (uny.centerPosition), uny.Extents, uny.transform.rotation, finalMask);
            HashSet<Collider> collidersHash = new HashSet<Collider> (colliders);
            
            // Check with connected cluster
            uny.shardList = new List<RFShard>();
            if (uny.rigidHost.objectType == ObjectType.ConnectedCluster)
                for (int i = 0; i < uny.rigidHost.physics.clusterColliders.Count; i++)
                    if (uny.rigidHost.physics.clusterColliders[i] != null)
                        if (collidersHash.Contains (uny.rigidHost.physics.clusterColliders[i]) == true)
                        {
                            SetShardUnyState (uny.rigidHost.clusterDemolition.cluster.shards[i], uny.unyielding, uny.activatable);
                            uny.shardList.Add (uny.rigidHost.clusterDemolition.cluster.shards[i]);
                        }
        }
        
        // Get combined layer mask
        static int ClusterLayerMask(RayfireRigid rigid)
        {
            int mask = 0;
            if (rigid.objectType == ObjectType.ConnectedCluster)
                for (int i = 0; i < rigid.physics.clusterColliders.Count; i++)
                    if (rigid.physics.clusterColliders[i] != null)
                        mask = mask | 1 << rigid.clusterDemolition.cluster.shards[i].tm.gameObject.layer;
            return mask;
        }
        
        // Set unyielding state
        static void SetShardUnyState (RFShard shard, bool unyielding, bool activatable)
        {
            shard.uny = unyielding;
            shard.act = activatable;
        }
        
        /// /////////////////////////////////////////////////////////
        /// Mesh Root setup
        /// /////////////////////////////////////////////////////////
        
        // Set clusterized rigids uny state and mesh root rigids
        public static void MeshRootSetup (RayfireRigid mRoot)
        {
            // Get uny list
            List<RayfireUnyielding> unyList = GetUnyList (mRoot.transform);
            
            // Iterate very unyielding component
            for (int i = 0; i < unyList.Count; i++)
                SetMeshRootUnyRigidList (mRoot, unyList[i]);
            
            // Set rigid list uny and sim states 
            SetMeshRootUny (mRoot.transform, unyList);
        }
        
        // Get uny list
        static List<RayfireUnyielding> GetUnyList (Transform tm)
        {
            List<RayfireUnyielding> unyList = tm.GetComponents<RayfireUnyielding>().ToList();
            for (int i = unyList.Count - 1; i >= 0; i--)
                if (unyList[i].enabled == false)
                    unyList.RemoveAt (i);
            return unyList;
        }
        
        // Set uny state for mesh root rigids. Used by Mesh Root. Can be used for cluster shards
        static void SetMeshRootUnyRigidList (RayfireRigid mRoot, RayfireUnyielding uny)
        {
            // Get target mask
            int               finalMask     = MeshRootLayerMask(mRoot);
            Collider[]        colliders     = Physics.OverlapBox (uny.transform.TransformPoint (uny.centerPosition), uny.Extents, uny.transform.rotation, finalMask);
            HashSet<Collider> collidersHash = new HashSet<Collider> (colliders);
            
            // Check with connectivity rigids
            uny.rigidList = new List<RayfireRigid>();
            for (int i = 0; i < mRoot.fragments.Count; i++)
                if (mRoot.fragments[i].physics.meshCollider != null)
                    if (collidersHash.Contains (mRoot.fragments[i].physics.meshCollider) == true)
                        uny.rigidList.Add (mRoot.fragments[i]);
        }
        
        // Get combined layer mask
        static int MeshRootLayerMask(RayfireRigid mRoot)
        {
            int mask = 0;
            for (int i = 0; i < mRoot.fragments.Count; i++)
                if (mRoot.fragments[i].physics.meshCollider != null)
                    mask = mask | 1 << mRoot.fragments[i].gameObject.layer;
            return mask;
        }
        
        // Set rigid list uny and sim states 
        public static void SetMeshRootUny (Transform tm, List<RayfireUnyielding> unyList)
        {
            // Get uny list
            if (unyList == null)
                unyList = GetUnyList (tm);
            
            // Iterate uny components list
            for (int c = 0; c < unyList.Count; c++)
            {
                // No rigids
                if (unyList[c].rigidList.Count == 0)
                    continue;

                // Set uny and act states for Rigids
                SetRigidUnyState (unyList[c]);
                
                // Set simulation type by
                SetRigidUnySim (unyList[c]);
            }
        }
        
        // Set unyielding state
        static void SetRigidUnyState (RayfireUnyielding uny)
        {
            // Common ops> Editor and Runtime
            for (int i = 0; i < uny.rigidList.Count; i++)
            {
                uny.rigidList[i].activation.unyielding  = uny.unyielding;
                uny.rigidList[i].activation.activatable = uny.activatable;
            }

            // Runtime ops.
            if (Application.isPlaying == true)
            {
                for (int i = 0; i < uny.rigidList.Count; i++)
                {
                    // Set uny id
                    if (uny.rigidList[i].activation.unyList == null)
                        uny.rigidList[i].activation.unyList = new List<int>();

                    // Collects
                    uny.rigidList[i].activation.unyList.Add (uny.id);

                    // Stop velocity and offset activation coroutines for not activatable uny objects 
                    if (uny.unyielding == true && uny.activatable == false)
                    {
                        if (uny.rigidList[i].activation.velocityEnum != null )
                            uny.rigidList[i].StopCoroutine (uny.rigidList[i].activation.velocityEnum);
                        if (uny.rigidList[i].activation.offsetEnum != null )
                            uny.rigidList[i].StopCoroutine (uny.rigidList[i].activation.offsetEnum);
                    }
                }
            }
        }
        
        // Set unyielding rigids sim type by
        static void SetRigidUnySim (RayfireUnyielding uny)
        {
            if (Application.isPlaying == true && uny.simulationType != UnySimType.Original)
                for (int i = 0; i < uny.rigidList.Count; i++)
                {
                    uny.rigidList[i].simulationType = (SimType)uny.simulationType;
                    RFPhysic.SetSimulationType (uny.rigidList[i].physics.rigidBody, uny.rigidList[i].simulationType,
                        ObjectType.Mesh, uny.rigidList[i].physics.useGravity, uny.rigidList[i].physics.solverIterations);
                }
        }
        
        // Set unyielding state
        public static void SetSlicedRigidUnyState (RayfireRigid rigid, int unyId, bool uny, bool act)
        {
            rigid.activation.unyielding  = uny;
            rigid.activation.activatable = act;

            // Set uny id
            if (rigid.activation.unyList == null)
                rigid.activation.unyList = new List<int>();

            // Collects
            rigid.activation.unyList.Add (unyId);

            // Stop velocity and offset activation coroutines for not activatable uny objects 
            if (uny == true && act == false)
            {
                if (rigid.activation.velocityEnum != null)
                    rigid.StopCoroutine (rigid.activation.velocityEnum);
                if (rigid.activation.offsetEnum != null)
                    rigid.StopCoroutine (rigid.activation.offsetEnum);
            }
        }

        /// /////////////////////////////////////////////////////////
        /// Rigid Root Setup
        /// /////////////////////////////////////////////////////////
        
        // Set uny state for mesh root rigids. Used by Mesh Root. Can be used for cluster shards
        public void GetRigidRootUnyShardList(RayfireRigidRoot rigidRoot)
        {
            // Uny disabled
            if (enabled == false)
                return;

            // Get target mask TODO check fragments layer
            int mask = 0;
            
            // Check with rigid root shards colliders
            for (int i = 0; i < rigidRoot.cluster.shards.Count; i++)
                if (rigidRoot.cluster.shards[i].col != null)
                    mask = mask | 1 << rigidRoot.cluster.shards[i].tm.gameObject.layer;
                            
            // Get box overlap colliders
            Collider[]        colliders     = Physics.OverlapBox (transform.TransformPoint (centerPosition), Extents, transform.rotation, mask);
            HashSet<Collider> collidersHash = new HashSet<Collider> (colliders);

            // Check with rigid root shards colliders
            shardList = new List<RFShard>();
            for (int i = 0; i < rigidRoot.cluster.shards.Count; i++)
                if (rigidRoot.cluster.shards[i].col != null)
                    if (collidersHash.Contains (rigidRoot.cluster.shards[i].col) == true)
                        shardList.Add (rigidRoot.cluster.shards[i]);
        }
        
        // Set sim amd uny states for cached shards
        public void SetRigidRootUnyShardList()
        {
            // No shards
            if (shardList.Count == 0)
                return;
            
            // Iterate cached shards
            for (int i = 0; i < shardList.Count; i++)
            {
                // Set uny states
                shardList[i].uny = unyielding;
                shardList[i].act = activatable;
                
                // Set sim states
                if (simulationType != UnySimType.Original)
                    shardList[i].sm = (SimType)simulationType;
            }

            // TODO Stop velocity and offset activation coroutines for not activatable uny objects (copy from above)
        }
        
        /// /////////////////////////////////////////////////////////
        /// Activate
        /// /////////////////////////////////////////////////////////
        
        // Activate inactive\kinematic shards/fragments
        public void Activate()
        {
            // Activate all rigids, init connectivity check after last activation, nullify connectivity for every
            if (HasRigids == true)
            {
                for (int i = 0; i < rigidList.Count; i++)
                {
                    // Activate if activatable
                    if (rigidList[i].activation.activatable == true)
                    {
                        rigidList[i].Activate (i == rigidList.Count - 1);
                        rigidList[i].activation.connect = null;
                    }
                }
            }

            // Activate connected clusters shards
            if (HasShards == true)
            {
                // Collect shards colliders
                List<Collider> colliders = new List<Collider>();
                for (int i = 0; i < shardList.Count; i++)
                    if (shardList[i].col != null)
                        colliders.Add (shardList[i].col);

                // No colliders
                if (colliders.Count == 0)
                    return;
                
                // Get Unyielding shards
                List<RFShard> shards = RFDemolitionCluster.DemolishConnectedCluster (rigidHost, colliders.ToArray());

                // Activate
                if (shards != null && shards.Count > 0)
                    for (int i = 0; i < shards.Count; i++)
                        RFActivation.ActivateShard (shards[i], null);
            }
        }

        /// /////////////////////////////////////////////////////////
        /// Manager register
        /// /////////////////////////////////////////////////////////
        
        // Register in manager
        void Register()
        {
            // TODO prevent double registering
            
            RFUny uny = new RFUny();
            uny.id       = GetUnyId();
            uny.scr      = this;
            uny.size     = Extents;

            uny.center   = transform.TransformPoint (centerPosition);
            uny.rotation = transform.rotation;

            // Add in all uny list
            RayfireMan.inst.unyList.Add (uny);

            // Save uny id to this id
            id = uny.id;
        }
        
        // Get uniq id
        static int GetUnyId()
        {
            return RayfireMan.inst.unyList.Count + 1;
        }
        
        /// /////////////////////////////////////////////////////////
        /// Getters
        /// /////////////////////////////////////////////////////////
        
        // Had child cluster
        bool HasRigids { get { return rigidList != null && rigidList.Count > 0; } }
        bool HasShards { get { return shardList != null && shardList.Count > 0; } }
        
        // Get final extents
        Vector3 Extents
        {
            get
            {
                Vector3 ext = size / 2f;
                ext.x *= transform.lossyScale.x;
                ext.y *= transform.lossyScale.y;
                ext.z *= transform.lossyScale.z;
                return ext;
            }
        }
    }

    [Serializable]
    public class RFUny
    {
        public int               id;
        public RayfireUnyielding scr;
        
        public Vector3    size;
        public Vector3    center;
        public Quaternion rotation;
    }
}