using Kuhpik;
using UnityEngine;

public class LoseAppearSystem : GameSystemWithScreen<LoseUIScreen>
{
    public override void OnStateEnter()
    {
        screen.RestartButton.onClick.AddListener(GameRestart);
    }

    private void GameRestart()
    {
        Bootstrap.Instance.GameRestart(0);
    }
}