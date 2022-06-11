using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OnTriggerEnterComponent onTriggerEnter;

    public OnTriggerEnterComponent OnTriggerEnter => onTriggerEnter;

    private const string PUNCH = "Punch";
    private const string START_BATTLE = "StartBattle";

    private int kickHash;
    private int startBattleHash;

    private float currentHealth;

    private Rigidbody[] rigidbodies;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        kickHash = Animator.StringToHash(PUNCH);
        startBattleHash = Animator.StringToHash(START_BATTLE);    
    }

    public void DoPunch()
    {
        animator.SetTrigger(kickHash);
    }
    public void StartBattle()
    {
        animator.SetTrigger(startBattleHash);
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

    public void StopAnimator()
    {
        animator.StopPlayback();
    }
}