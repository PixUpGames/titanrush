using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent: MonoBehaviour
{
    public NavMeshAgent NavMesh;
    public OnTriggerEnterComponent OnTriggerEnterComp;
    public PlayerCanvasComponent PlayerCanvas;
    public PlayerAnimatorComponent PlayerAnimator;
}