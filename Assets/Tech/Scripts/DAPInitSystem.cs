using Kuhpik;
using Supyrb;
using UnityEngine;

public class DAPInitSystem : GameSystem
{
    private ChangeEnemyStateSignal signal;
    public override void OnInit()
    {
        game.enemyBoss.Prepare();
        game.PlayerComponent.PlayerAnimator.SetFightIdle(true);
        game.PlayerComponent.NavMesh.enabled = false;
        game.PlayerComponent.RB.isKinematic = true;
    }

}