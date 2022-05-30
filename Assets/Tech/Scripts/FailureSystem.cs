using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class FailureSystem : GameSystem
{
    [SerializeField, Tag]
    private string obstacleTag;

    public override void OnStateEnter()
    {
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += OnObstacleEnter;
    }

    private void OnObstacleEnter(Transform other, Transform @object)
    {
        if (other.CompareTag(obstacleTag))
        {
            game.PlayerComponent.OnTriggerEnterComp.OnEnter -= OnObstacleEnter;

            ChangeToFailState();
        }
    }
    private void ChangeToFailState()
    {
        game.PlayerComponent.NavMesh.isStopped = true;

        Debug.LogWarning("INCREASING LEVEL NUMBER");
        player.Level++;

        Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
    }
}
