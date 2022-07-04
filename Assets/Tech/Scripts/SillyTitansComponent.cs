using NaughtyAttributes;
using UnityEngine;

public class SillyTitansComponent : CuttableItem
{
    [SerializeField] private float speed;
    [SerializeField] private int reward;
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Animator animator;
    [SerializeField] private OnTriggerEnterComponent onTriggerEnterComponent;
    [SerializeField, Tag] private string playerTag;
    [SerializeField] private CollectableComponent coin;

    public override CuttableType GetCuttableType() => CuttableType.Silly_Titan;
    public override int ReceiveAward() => reward;

    private bool activated = false;

    private void Awake()
    {
        onTriggerEnterComponent.OnEnter += OnPlayerEnter;
    }
    private void Update()
    {
        if (!activated)
        {
            return;
        }

        transform.parent.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    public override void Cut()
    {
        activated = false;

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
            activated = true;

            animator.SetBool("Run", true);
        }
    }
}