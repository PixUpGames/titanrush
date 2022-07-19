using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class FailureSystem : GameSystem
{
    [SerializeField, Tag]
    private string obstacleTag;

    [SerializeField] private float loseDelay;

    public override void OnStateEnter()
    {
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += OnObstacleEnter;
    }

    private void OnObstacleEnter(Transform other, Transform @object)
    {
        if (other.CompareTag(obstacleTag))
        {
            game.PlayerComponent.OnTriggerEnterComp.OnEnter -= OnObstacleEnter;

            game.PlayerComponent.PlayerAnimator.Die();

            StartCoroutine(ChangeToFailState());
        }
    }

    private IEnumerator ChangeToFailState()
    {
        game.PlayerComponent.NavMesh.enabled = false ;

        yield return new WaitForSeconds(loseDelay);
        Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
    }
}
