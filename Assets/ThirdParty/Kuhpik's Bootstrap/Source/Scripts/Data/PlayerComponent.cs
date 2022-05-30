using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent: MonoBehaviour
{
    public NavMeshAgent NavMesh;
    public OnTriggerEnterComponent OnTriggerEnterComp;
    public PlayerCanvasComponent PlayerCanvas;
    public PlayerAnimatorComponent PlayerAnimator;

    private float currentHealth;

    public void SetHealth(float value)
    {
        currentHealth = value;
    }
    public void ReceiveDamage(float value)
    {
        currentHealth -= value;
    }
    public float GetHealth()
    {
        return currentHealth;
    }
}