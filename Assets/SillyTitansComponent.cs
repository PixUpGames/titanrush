using NaughtyAttributes;
using UnityEngine;

public class SillyTitansComponent : CuttableItem
{
    [SerializeField] private int reward;
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Animator animator;
    [SerializeField] private OnTriggerEnterComponent onTriggerEnterComponent;
    [SerializeField, Tag] private string playerTag;

    public override CuttableType GetCuttableType() => CuttableType.Silly_Titan;
    public override int ReceiveAward() => reward;

    private void Awake()
    {
        onTriggerEnterComponent.OnEnter += OnPlayerEnter;
    }
    public override void Cut()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
            animator.enabled = false;
        }
    }

    private void OnPlayerEnter(Transform other, Transform @object)
    {
        if (other.CompareTag(playerTag))
        {
            animator.SetBool("Run", true);
        }
    }
}