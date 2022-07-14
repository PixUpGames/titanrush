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
    [SerializeField] private ShopItemConfig[] hatsConfigs;
    private IEnumerator enemyRoutine;

    public override void OnInit()
    {
        game.Multiplier = 5f;
        screen.multiplyText.text = "x" + game.Multiplier.ToString();
        dapAnim = UIManager.GetUIScreen<FightingScreenUI>().DapAnim;
        tapAnim = UIManager.GetUIScreen<FightingScreenUI>().TapAnim;

        Signals.Get<PlayerHitSignal>().AddListener(FinishImmediate);

        if (enemyRoutine != null)
        {
            StopCoroutine(enemyRoutine);
        }
        enemyRoutine = HammerBossRoutine();
        StartCoroutine(enemyRoutine);

        if (player.TempItem != CustomizableType.Null)
            InitTempItemScreen();
    }

    private void InitTempItemScreen()
    {
        foreach (var item in hatsConfigs)
        {
            if (item.CustomizableType == player.TempItem)
            {
                screen.TempItemIcon.sprite = item.Icon;
            }
        }

        screen.GetItemButton.onClick.AddListener(GetItem);
        screen.LoseItemButton.onClick.AddListener(LoseItem);
    }

    private void GetItem()
    {
        if (player.TempItem != CustomizableType.Null)
        {
            if (!player.OpenedCustomizables.Contains(player.TempItem))
            {
                player.OpenedCustomizables.Add(player.TempItem);
                player.hatType = player.TempItem;
            }
        }
        player.TempItem = CustomizableType.Null;
        Finish();
    }

    private void LoseItem()
    {
        player.TempItem = CustomizableType.Null;
        Finish();
    }

    public void OnFootKickFeedback()
    {
        game.Multiplier += 1.2f;
        Mathf.Sign(game.Multiplier);
        screen.multiplyText.text = "x" + game.Multiplier.ToString("0.0");
        screen.multiplyText.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f);
    }

    private IEnumerator HammerBossRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (game.enemyBoss.GetHealth() > 0 || game.punchAndDodgeState == EnemyState.DEATH)
        {
            dapAnim.SetActive(true);
            tapAnim.SetActive(false);
            game.enemyBoss.DoResetStun();

            if (game.enemyBoss.GetHealth() > 0)
            {
                yield return new WaitForSeconds(attackStepDelay / 3);
            }

            for (int i = 0; i < 3; i++)
            {
                if (game.enemyBoss.GetHealth() <= 0)
                    break;

                dapAnim.SetActive(true);
                tapAnim.SetActive(false);
                var finishComp = (HammerFinishComponent)game.Finish;
                finishComp.BigTitan.transform.DOLookAt(game.PlayerComponent.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(HammerHit);
                yield return new WaitForSeconds(attackStepDelay + 1);
            }

            if (game.enemyBoss.GetHealth() > 0)
            {
                var finishComp = (HammerFinishComponent)game.Finish;
                finishComp.BigTitan.transform.DOLookAt(game.PlayerComponent.transform.position, 0.5f).SetEase(Ease.Linear);
                yield return new WaitForSeconds(attackStepDelay / 3);
            }

            SetStunAfterAttack();
            yield return new WaitForSeconds(attackStepDelay);
        }

        UIManager.GetUIScreen<FightingScreenUI>().hpBarHolder.gameObject.SetActive(false);
        game.enemyBoss.DoResetStun();
        game.enemyBoss.SetKneel(true);
        tapAnim.SetActive(true);
        dapAnim.SetActive(false);
        game.punchAndDodgeState = EnemyState.DEATH;
        screen.DapBar.gameObject.SetActive(true);
        screen.DapBar.value = screen.DapBar.maxValue;
        screen.DapBar.DOValue(screen.DapBar.minValue, 8f).OnComplete(() => { game.enemyBoss.DoDeath(); screen.DapBar.gameObject.SetActive(false); StartCoroutine(FinishRoutine()); });
    }

    IEnumerator FinishRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        if (player.TempItem != CustomizableType.Null)
        {
            screen.TempItemScreen.SetActive(true);
        }
        else
        {
            Finish();
        }
    }

    private void FinishImmediate()
    {
        if (game.enemyBoss.GetHealth() <= 0)
        {
            if (enemyRoutine != null)
            {
                StopCoroutine(enemyRoutine);
            }

            UIManager.GetUIScreen<FightingScreenUI>().hpBarHolder.gameObject.SetActive(false);
            game.enemyBoss.DoResetStun();
            game.enemyBoss.DoResetHammerHit();
            game.enemyBoss.SetKneel(true);
            tapAnim.SetActive(true);
            dapAnim.SetActive(false);
            game.punchAndDodgeState = EnemyState.DEATH;
            screen.DapBar.gameObject.SetActive(true);
            screen.DapBar.value = screen.DapBar.maxValue;
            screen.DapBar.DOValue(screen.DapBar.minValue, 8f).OnComplete(() => { game.enemyBoss.DoDeath(); screen.DapBar.gameObject.SetActive(false); StartCoroutine(FinishRoutine()); });
        }
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
        if (game.enemyBoss.GetHealth() > 0)
        {
            dapAnim.SetActive(false);
            tapAnim.SetActive(true);
            game.punchAndDodgeState = EnemyState.STUNNED;
            game.enemyBoss.DoStun();
        }
    }

    private void Finish()
    {
        Bootstrap.Instance.ChangeGameState(GameStateID.Win);

    }
}

    public enum EnemyState
{
    PUNCH = 1,
    STUNNED = 2,
    DEATH = 3
}