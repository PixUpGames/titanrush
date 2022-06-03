using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private float explosionForce = 200f;
    [SerializeField] private Vector3 explosionOffset;

    void Start()
    {
        var bricks = Physics.OverlapSphere(transform.position + Vector3.up, 10f, blockLayer);
        
        foreach (var brick in bricks)
        {
            brick.attachedRigidbody.AddExplosionForce(explosionForce, transform.position + explosionOffset, 10f, 1, ForceMode.Force);
            brick.attachedRigidbody.AddTorque(Vector3.back, ForceMode.VelocityChange);
        }
    }
}