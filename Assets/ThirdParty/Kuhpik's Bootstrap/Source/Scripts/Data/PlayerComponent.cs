using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent: MonoBehaviour
{
    [SerializeField] private Vector3[] mutationScales;

    public NavMeshAgent NavMesh;
    public OnTriggerEnterComponent OnTriggerEnterComp;
    public PlayerCanvasComponent PlayerCanvas;
    public PlayerAnimatorComponent PlayerAnimator;

    private float currentHealth;

    private int currentMutation = 0;

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

    public void Mutate()
    {
        if (currentMutation >= mutationScales.Length)
        {
            return;
        }

        transform.localScale = mutationScales[currentMutation];

        currentMutation++;
    }
}