using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGateComponent : MonoBehaviour
{
    [SerializeField] private GameObject staticGate;
    [SerializeField] private GameObject dynamicGate;
    [SerializeField] private bool isGate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyComponent enemy)||other.TryGetComponent(out PlayerComponent player))
        {
            staticGate.SetActive(false);
            dynamicGate.SetActive(true);

            if(other.TryGetComponent(out PlayerComponent playerComponent) && isGate==true)
                StartCoroutine(BreakWallRoutine(playerComponent));
        }
    }

    private IEnumerator BreakWallRoutine(PlayerComponent player)
    {
        player.PlayerAnimator.Animator.SetLayerWeight(1, 1);
        player.PlayerAnimator.Animator.SetTrigger("Break");
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.7f);
        player.PlayerAnimator.Animator.SetLayerWeight(1, 0);
    }
}
