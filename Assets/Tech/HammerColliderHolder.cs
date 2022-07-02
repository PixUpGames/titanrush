using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerColliderHolder : MonoBehaviour
{
    [SerializeField] Collider hammerCollider;
    public Collider Collider => hammerCollider;
    public void ToggleHammerCollider(int value)
    {
        if (value==1)
        {
            hammerCollider.enabled = true;
        }
        else
        {
            hammerCollider.enabled = false;
        }
    }
}
