using UnityEngine;

public class CuttableHandComponent : CuttableItem
{
    [SerializeField] private int award = 20;
    [SerializeField] private Rigidbody[] fingers;
    [SerializeField] private ParticleSystem[] VFX;

    public override int ReceiveAward() => award;
    public override CuttableType GetCuttableType() => CuttableType.Hand;

    public override void Cut()
    {
        foreach (var finger in fingers)
        {
            finger.isKinematic = false;
        }

        foreach (var vfx in VFX)
        {
            vfx.Play();
        }
    }
}