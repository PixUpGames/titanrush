using Kuhpik;
using UnityEngine;

public class MoveCharacterToPointSystem : GameSystem
{
    [SerializeField] private float changeStateDistance= .5f;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.gameObject.SetActive(false);

        game.PlayerComponent.NavMesh.SetDestination(game.Finish.characterFinishPoint.position);

        game.PlayerComponent.DisableWindVFX();
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(game.PlayerComponent.transform.position, game.Finish.characterFinishPoint.position) <= changeStateDistance)
        {
            SwitchToFight();
        }
    }

    private void SwitchToFight()
    {
        if (game.PlayerComponent.Mutations <= 0)
        {
            Bootstrap.Instance.ChangeGameState(GameStateID.PlayerDeath);

            return;
        }

        switch (game.LevelConfig.FinishState)
        {
            case FinishState.FIGHTING:
                {
                    Bootstrap.Instance.ChangeGameState(GameStateID.Fighting);
                    break;
                }
            case FinishState.DODGE_AND_PUNCH:
                {
                    Bootstrap.Instance.ChangeGameState(GameStateID.DodgeAndPunch);
                    break;
                }
        }
    }
}
public enum FinishState
{
    DODGE_AND_PUNCH = 1,
    FIGHTING = 2
}