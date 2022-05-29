using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string FIGHT_IDLE = "Fight";
    private const string KICK = "Kick";

    private int fightIdleHash;
    private int kickHash;

    private void Awake()
    {
        fightIdleHash = Animator.StringToHash(FIGHT_IDLE);
        kickHash = Animator.StringToHash(KICK);
    }

    public void SetFightIdle(bool value)
    {
        animator.SetBool(fightIdleHash, value);
    }
    public void SetKickAnimation()
    {
        animator.SetTrigger(kickHash);
    }
}