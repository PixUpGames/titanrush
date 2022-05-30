using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string PUNCH = "Kick";

    private int kickHash;

    private float currentHealth;

    private void Awake()
    {
        kickHash = Animator.StringToHash(PUNCH);    
    }

    public void DoPunch()
    {
        animator.SetTrigger(kickHash);
    }

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