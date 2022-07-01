using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBreakableWallComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody[] bricks;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerComponent player))
        {
            foreach (var brick in bricks)
            {
                brick.isKinematic = false;
            }
        }
    }
}
