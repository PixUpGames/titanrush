using DG.Tweening;
using Kuhpik;
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
    }

    private void GameRestart()
    {
        Bootstrap.Instance.GameRestart(0);
    }
}