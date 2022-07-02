using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGateComponent : MonoBehaviour
{
    [SerializeField] private GameObject staticGate;
    [SerializeField] private GameObject dynamicGate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyComponent enemy)||other.TryGetComponent(out PlayerComponent player))
        {
            staticGate.SetActive(false);
            dynamicGate.SetActive(true);
        }
    }
}
