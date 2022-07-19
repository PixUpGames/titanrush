using DG.Tweening;
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
        game.punchAndDodgeState = EnemyState.PUNCH;

        if (game.MutationLevel == 1)
        {
            game.PlayerComponent.PlayerAnimator.CameraHolder.DOLocalMove(new Vector3(0.2f, 0, -0.602f), 0.5f);

        }
        if (game.MutationLevel == 2)
        {
            game.PlayerComponent.PlayerAnimator.CameraHolder.DOLocalMove(new Vector3(0.5f, 1.29f, -1.22f), 0.5f);
        }
    }
}