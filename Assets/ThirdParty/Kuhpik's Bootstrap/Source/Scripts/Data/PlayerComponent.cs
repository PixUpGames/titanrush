using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(OnTriggerEnterComponent))]
public class PlayerComponent: MonoBehaviour
{
    public NavMeshAgent NavMesh;
    public OnTriggerEnterComponent OnTriggerEnterComponent;
}