using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerTriggerComponent : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerComponent player))
        {
            Signals.Get<PlayerHitSignal>().Dispatch();
        }
    }
}
