using UnityEngine;

public class CuttableTitanComponent : CuttableItem
{
    [SerializeField] private int award = 20;
    [SerializeField] private Rigidbody head;
    [SerializeField] private ParticleSystem[] VFX;

    public override int ReceiveAward() => award;

    public override void Cut()
    {
        head.isKinematic = false;

        foreach (var vfx in VFX)
        {
            vfx.Play();
        }
    }
}