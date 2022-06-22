using Kuhpik;
using Supyrb;
using UnityEngine;

public class DAPEnemyBehaviourSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<ChangeEnemyStateSignal>().AddListener(TriggerEnemy);  
    }

    private void TriggerEnemy(EnemyState enemyState)
    {
        if (enemyState == EnemyState.PUNCH)
        {
            var finishComp = (HammerFinishComponent) game.Finish;
            finishComp.BigTitan.transform.LookAt(game.PlayerComponent.transform.position);

            game.enemyBoss.DoHammerHit();
        }
    }
}

public enum EnemyState
{
    PUNCH = 1,
    STUNNED = 2,
    DEATH = 3
}