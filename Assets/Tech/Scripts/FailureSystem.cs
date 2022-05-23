using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class FailureSystem : GameSystem
{
    [SerializeField, Tag]
    private string obstacleTag;

    public override void OnStateEnter()
    {
        game.playerComponent.OnTriggerEnterComp.OnEnter += OnObstacleEnter;
    }

    private void OnObstacleEnter(Transform other, Transform @object)
    {
        if (other.CompareTag(obstacleTag))
        {
            game.playerComponent.OnTriggerEnterComp.OnEnter -= OnObstacleEnter;

            ChangeToFailState();
        }
    }
    private void ChangeToFailState()
    {
        game.playerComponent.NavMesh.isStopped = true;

        Debug.LogWarning("INCREASING LEVEL NUMBER");
        player.Level++;

        Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
    }
}
