using Kuhpik;
using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject finalPunchVFX;
    [SerializeField] private GameObject electricityVFX;
    [SerializeField] private Transform head;

    public Transform Head => head;

    private const string FIGHT_IDLE = "StartBattle";
    private const string KICK = "Kick";
    private const string PUNCH = "Punch";
    private const string RUN = "Run";
    private const string FINAL_KICK = "PunchFatality";
    private const string RECEIVE_DAMAGE = "TakeDamage";
    private const string DIE = "Death";

    private int fightIdleHash;
    private int kickHash;
    private int runHash;
    private int finalKickHash;
    private int punchHash;
    private int receiveDamageHash;
    private int deathHash;

    private void Awake()
    {
        fightIdleHash = Animator.StringToHash(FIGHT_IDLE);
        kickHash = Animator.StringToHash(KICK);
        runHash = Animator.StringToHash(RUN);
        finalKickHash = Animator.StringToHash(FINAL_KICK);
        punchHash = Animator.StringToHash(PUNCH);
        receiveDamageHash = Animator.StringToHash(RECEIVE_DAMAGE);
        deathHash = Animator.StringToHash(DIE);
    }

    public void SetFightIdle(bool value)
    {
        animator.SetBool(fightIdleHash, value);
        animator.SetBool(runHash, false);
    }
    public void SetKickAnimation()
    {
        animator.SetTrigger(kickHash);
    }
    public void SetRunAnimation(bool enable)
    {
        animator.SetBool(runHash, enable);
    }
    public void SetFinalKick()
    {
        animator.SetTrigger(finalKickHash);

        ActivateFinalPunchVFX();
    }
    public void FinishFatalityPunch()
    {
        Time.timeScale = 1f;
        Bootstrap.Instance.ChangeGameState(GameStateID.EnemyDefeated);
        finalPunchVFX?.SetActive(true);
    }
    public void ActivateFinalPunchVFX()
    {
        electricityVFX?.SetActive(true);
    }
    public void ClearAllAnimations()
    {
        animator.ResetTrigger(kickHash);
    }
    public void Punch()
    {
        animator.SetTrigger(punchHash);
    }
    public void ReceiveDamage()
    {
        animator.SetTrigger(receiveDamageHash);
    }
    public void Die()
    {
        animator.SetTrigger(deathHash);
    }
}