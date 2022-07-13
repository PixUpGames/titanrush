using Kuhpik;
using Supyrb;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OnTriggerEnterComponent onTriggerEnter;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private FXCaster fXCaster;
    public OnTriggerEnterComponent OnTriggerEnter => onTriggerEnter;

    private const string PUNCH = "Punch";
    private const string KICK = "Kick";
    private const string STOMP = "Stomp";
    private const string START_BATTLE = "StartBattle";
    private const string TAKE_DAMAGE = "TakeDamage";
    private const string FLY = "Fly";
    private const string KNEEL = "Kneel";
    private const string HAMMER = "HammerPunch";
    private const string STUN = "Stun";
    private const string DEATH = "Death";

    private int punchHash;
    private int kneelHash;
    private int kickHash;
    private int startBattleHash;
    private int takeDamageHash;
    private int flyHash;
    private int stompHash;
    private int hammerPunchHash;
    private int stunHash;
    private int deathHash;

    private float currentHealth;
    private Rigidbody[] rigidbodies;
    private EnemyHitSignal hitSignal;
    private HammerHitSignal hammerHitSignal;
    public FXCaster FXCaster => fXCaster;
    public Transform CameraHolder;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        punchHash = Animator.StringToHash(PUNCH);
        startBattleHash = Animator.StringToHash(START_BATTLE);
        takeDamageHash = Animator.StringToHash(TAKE_DAMAGE);
        flyHash = Animator.StringToHash(FLY);    
        kickHash = Animator.StringToHash(KICK);
        stompHash = Animator.StringToHash(STOMP);
        kneelHash = Animator.StringToHash(KNEEL);
        hammerPunchHash = Animator.StringToHash(HAMMER);
        stunHash = Animator.StringToHash(STUN);
        deathHash = Animator.StringToHash(DEATH);
        fXCaster = GetComponentInChildren<FXCaster>();
        hitSignal = Signals.Get<EnemyHitSignal>();
        hammerHitSignal = Signals.Get<HammerHitSignal>();
    }

    public virtual void Prepare()
    {
        StartBattle();
    }
    public void DoPunch()
    {
        animator.SetTrigger(punchHash);
    }
    public void DoDeath()
    {
        animator.SetTrigger(deathHash);
    }
    public void DoStun()
    {
        animator.SetTrigger(stunHash);
    }

    public void DoResetStun()
    {
        animator.ResetTrigger(stunHash);
    }

    public void DoKick()
    {
        animator.SetTrigger(kickHash);
    }
    public void SetKneel(bool enable)
    {
        animator.SetBool(kneelHash, enable);
    }
    public void DoHammerHit()
    {
        animator.SetTrigger(hammerPunchHash);
    }
    public void DoStomp()
    {
        animator.SetTrigger(stompHash);
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
        hitParticle?.Play();
        animator.SetTrigger(takeDamageHash);
        currentHealth -= value;
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    public void DisableAnimator()
    {
        animator.enabled = false;

        var thisRB = GetComponent<Rigidbody>();

        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
        }

        thisRB.isKinematic = true;
    }

    public void StopAnimator()
    {
        animator.StopPlayback();
    }
    public void FlyAway()
    {
        animator.SetBool(flyHash, true);
    }
    public void HitSignal()
    {
        hitSignal.Dispatch();
    }

    public void HammerHitSignal()
    {
        hammerHitSignal.Dispatch();
    }
}