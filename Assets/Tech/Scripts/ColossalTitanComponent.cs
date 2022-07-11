using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColossalTitanComponent : MonoBehaviour
{
    public Animator ColossalAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerComponent player))
        {
            ColossalAnimator.SetTrigger("Attack");
        }
    }
}
