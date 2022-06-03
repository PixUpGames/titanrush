using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{
    [SerializeField] private LayerMask blockLayer; 
    void Start()
    {
        var bricks = Physics.OverlapSphere(transform.position + Vector3.up, 2f, blockLayer);
        
        foreach (var brick in bricks)
        {
            brick.attachedRigidbody.AddExplosionForce(200f, transform.position + Vector3.up, 2f);
        }
    }
}