using Kuhpik;
using Supyrb;
using System.Collections;
using UnityEngine;

public class DAPEnemyBehaviourSystem : GameSystem
{
    [SerializeField] private float attackStepDelay;
    public override void OnInit()
    {
        Signals.Get<ChangeEnemyStateSignal>().AddListener(TriggerEnemy);
    }

    private void TriggerEnemy(EnemyState enemyState)
    {
        //if (enemyState == EnemyState.PUNCH)
        //{
        //    var finishComp = (HammerFinishComponent) game.Finish;
        //    finishComp.BigTitan.transform.LookAt(game.PlayerComponent.transform.position);

        //    game.enemyBoss.DoHammerHit();
        //}

        StartCoroutine(HammerBossRoutine());
    }

    private IEnumerator HammerBossRoutine()
    {
        while (game.punchAndDodgeState != EnemyState.DEATH)
        {
            yield return new WaitForSeconds(attackStepDelay);
            HammerHit();
            yield return new WaitForSeconds(attackStepDelay);
            SetStunAfterAttack();
        }
    }

    private void HammerHit()
    {
        game.punchAndDodgeState = EnemyState.PUNCH;
        var finishComp = (HammerFinishComponent)game.Finish;
        finishComp.BigTitan.transform.LookAt(game.PlayerComponent.transform.position);

        game.enemyBoss.DoHammerHit();
    }

    private void SetStunAfterAttack()
    {
        game.punchAndDodgeState = EnemyState.STUNNED;
        var finishComp = (HammerFinishComponent)game.Finish;
        game.enemyBoss.SetKneel(true);
    }
}

public enum EnemyState
{
    PUNCH = 1,
    STUNNED = 2,
    DEATH = 3
}