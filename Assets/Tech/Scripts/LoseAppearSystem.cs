using DG.Tweening;
using Kuhpik;
using System.Collections;
using UnityEngine;

public class LoseAppearSystem : GameSystemWithScreen<LoseUIScreen>
{
    [SerializeField]
    private float loseAppearTime = 1f;
    public override void OnStateEnter()
    {
        // Screen Fade In
        screen.ScreenCanvasGroup.DOFade(0, 0).OnComplete(
                () => screen.ScreenCanvasGroup.DOFade(1, loseAppearTime)
            );

        screen.RestartButton.onClick.AddListener(GameRestart);
        screen.ReviveButton.onClick.AddListener(Revive);
        screen.fillTimer.DOFillAmount(0, 5f).SetEase(Ease.Linear).OnComplete(() => screen.RestartButton.gameObject.SetActive(true));

        StartCoroutine(CountDown());

        player.isRevive = true;
        player.RevivePos = game.PlayerComponent.transform.position + Vector3.back *-5f;
    }

    private void GameRestart()
    {
        player.isRevive = false;
        Bootstrap.Instance.GameRestart(0);
    }

    private void Revive()
    {
        Bootstrap.Instance.SaveGame();
        Debug.Log("[REWARD] Revive");
        Bootstrap.Instance.GameRestart(0);
    }

    private IEnumerator CountDown()
    {
        for (int i = 6 - 1; i >= 0; i--)
        {
            screen.timer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}