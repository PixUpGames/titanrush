using Kuhpik;
using Supyrb;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DAPEnemyBehaviourSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField] private float attackStepDelay;
    public override void OnInit()
    {
        game.Multiplier = 5f;
        screen.multiplyText.text = "x" + game.Multiplier.ToString();
        StartCoroutine(HammerBossRoutine());
    }

    public void OnFootKickFeedback()
    {
        game.Multiplier += 1.2f;
        Mathf.Sign(game.Multiplier);
        screen.multiplyText.text="x" + game.Multiplier.ToString("0.0");
        screen.multiplyText.transform.DOPunchScale(Vector3.one *0.1f, 0.3f);
    }

    private IEnumerator HammerBossRoutine()
    {
        while (game.enemyBoss.GetHealth() > 0 || game.punchAndDodgeState == EnemyState.DEATH)
        {
            if (game.enemyBoss.GetHealth() > 0)
            {
                yield return new WaitForSeconds(attackStepDelay/3);
            }

            var finishComp = (HammerFinishComponent)game.Finish;
            finishComp.BigTitan.transform.DOLookAt(game.PlayerComponent.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(HammerHit);

            if (game.enemyBoss.GetHealth() > 0)
            {
                yield return new WaitForSeconds(attackStepDelay + 1);
            }

            SetStunAfterAttack();
            yield return new WaitForSeconds(attackStepDelay);
        }

        UIManager.GetUIScreen<FightingScreenUI>().gameObject.SetActive(false);
        game.enemyBoss.DoResetStun();
        game.punchAndDodgeState = EnemyState.DEATH;
        screen.DapBar.gameObject.SetActive(true);
        screen.DapBar.value = screen.DapBar.maxValue;
        screen.DapBar.DOValue(screen.DapBar.minValue, 8f).OnComplete(() => { game.enemyBoss.DoDeath(); screen.DapBar.gameObject.SetActive(false);StartCoroutine(FinishRoutine()); });
        game.enemyBoss.SetKneel(true);
    }

    IEnumerator FinishRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Bootstrap.Instance.ChangeGameState(GameStateID.Win);
    }

    private void HammerHit()
    {
        if (game.enemyBoss.GetHealth() > 0)
        {
            game.enemyBoss.FXCaster.StopFX(5);
            game.punchAndDodgeState = EnemyState.PUNCH;

            game.enemyBoss.DoHammerHit();
        }
    }

    private void SetStunAfterAttack()
    {
        game.punchAndDodgeState = EnemyState.STUNNED;
        var finishComp = (HammerFinishComponent)game.Finish;
        game.enemyBoss.DoStun();
    }
}

public enum EnemyState
{
    PUNCH = 1,
    STUNNED = 2,
    DEATH = 3
}