using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string PUNCH = "Kick";

    private int kickHash;

    private float currentHealth;

    private Rigidbody[] rigidbodies;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
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
    public void DisableAnimator()
    {
        animator.enabled = false;

        foreach (var rb in rigidbodies)
        {
            rb.velocity = Vector3.zero;
        }
    }
}