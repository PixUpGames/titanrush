using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string PUNCH = "Kick";

    private int kickHash;

    private void Awake()
    {
        kickHash = Animator.StringToHash(PUNCH);    
    }

    public void DoPunch()
    {
        animator.SetTrigger(kickHash);
    }
}