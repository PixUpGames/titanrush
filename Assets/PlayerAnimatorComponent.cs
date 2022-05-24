using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string FIGHT_IDLE = "Fight Idle";

    private int fightIdleHash;

    private void Awake()
    {
        fightIdleHash = Animator.StringToHash(FIGHT_IDLE);
    }

    public void SetFightIdle()
    {
        animator.SetTrigger(fightIdleHash);
    }
}