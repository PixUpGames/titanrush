using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBreakableWallComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody[] bricks;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerComponent player) || other.TryGetComponent(out EnemyComponent enemy))
        {
            foreach (var brick in bricks)
            {
                brick.isKinematic = false;
            }

            if (other.TryGetComponent(out PlayerComponent playerComponent))
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
