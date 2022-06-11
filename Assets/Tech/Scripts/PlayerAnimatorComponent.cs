using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string FIGHT_IDLE = "StartBattle";
    private const string KICK = "Kick";
    private const string RUN = "Run";

    private int fightIdleHash;
    private int kickHash;
    private int runHash;

    private void Awake()
    {
        fightIdleHash = Animator.StringToHash(FIGHT_IDLE);
        kickHash = Animator.StringToHash(KICK);
        runHash = Animator.StringToHash(RUN);
    }

    public void SetFightIdle(bool value)
    {
        animator.SetBool(fightIdleHash, value);
    }
    public void SetKickAnimation()
    {
        animator.SetTrigger(kickHash);
    }
    public void SetRunAnimation(bool enable)
    {
        animator.SetBool(runHash, enable);
    }
}