using Kuhpik;
using Supyrb;
using UnityEngine;

public class DAPInitSystem : GameSystem
{
    private ChangeEnemyStateSignal signal;
    public override void OnInit()
    {
        signal = Signals.Get<ChangeEnemyStateSignal>();

        signal.AddListener(ChangeEnemyState);
    }
    public override void OnStateEnter()
    {
        game.enemyBoss.Prepare();
        game.PlayerComponent.PlayerAnimator.SetFightIdle(true);
        game.PlayerComponent.NavMesh.enabled = false;
        game.PlayerComponent.RB.isKinematic = true;

        signal.Dispatch(EnemyState.PUNCH);
    }

    private void ChangeEnemyState(EnemyState enemyState)
    {
        game.punchAndDodgeState = enemyState;
    }
}