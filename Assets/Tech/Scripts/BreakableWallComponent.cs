using UnityEngine;

public class BreakableWallComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Animate(int breakHash)
    {
        animator.SetTrigger(breakHash);
    }
}