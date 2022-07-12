using Kuhpik;
using Supyrb;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DAPEnemyBehaviourSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField] private float attackStepDelay;
    [SerializeField] private GameObject dapAnim;
    [SerializeField] private GameObject tapAnim;
    public override void OnInit()
    {
        game.Multiplier = 5f;
        screen.multiplyText.text = "x" + game.Multiplier.ToString();
        dapAnim = UIManager.GetUIScreen<FightingScreenUI>().DapAnim;
        tapAnim = UIManager.GetUIScreen<FightingScreenUI>().TapAnim;
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
            dapAnim.SetActive(true);
            tapAnim.SetActive(false);
            if (game.enemyBoss.GetHealth() > 0)
            {
                yield return new WaitForSeconds(attackStepDelay/3);
            }

            for (int i = 0; i < 3; i++)
            {
                dapAnim.SetActive(true);
                tapAnim.SetActive(false);
                var finishComp = (HammerFinishComponent)game.Finish;
                finishComp.BigTitan.transform.DOLookAt(game.PlayerComponent.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(HammerHit);
                yield return new WaitForSeconds(attackStepDelay+1);
            }

            if (game.enemyBoss.GetHealth() > 0)
            {
                var finishComp = (HammerFinishComponent)game.Finish;
                finishComp.BigTitan.transform.DOLookAt(game.PlayerComponent.transform.position, 0.5f).SetEase(Ease.Linear);
                yield return new WaitForSeconds(attackStepDelay/3);
            }

            SetStunAfterAttack();
            yield return new WaitForSeconds(attackStepDelay);
        }

        tapAnim.SetActive(true);
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
        //dapAnim.SetActive(true);
        if (game.enemyBoss.GetHealth() > 0)
        {
            game.enemyBoss.FXCaster.StopFX(5);
            game.punchAndDodgeState = EnemyState.PUNCH;

            game.enemyBoss.DoHammerHit();
        }
    }

    private void SetStunAfterAttack()
    {
        dapAnim.SetActive(false);
        tapAnim.SetActive(true);
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