using UnityEngine;

public class CuttableTitanComponent : CuttableItem
{
    [SerializeField] private int award = 20;
    [SerializeField] private Rigidbody head;
    [SerializeField] private ParticleSystem[] VFX;

    public override CuttableType GetCuttableType() => CuttableType.Head;
    public override int ReceiveAward() => award;

    public override void Cut()
    {
        head.isKinematic = false;

        head.AddForce(Vector3.up * 400f);
        head.AddTorque(Vector3.forward * 100f);

        foreach (var vfx in VFX)
        {
            vfx.Play();
        }
    }
}