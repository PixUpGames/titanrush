using Kuhpik;
using UnityEngine;

public class WinInitSystem : GameSystemWithScreen<WinUIScreen>
{
    [SerializeField] private float baseMultiplier = 10f;
    public override void OnStateEnter()
    {
        screen.InitScreen(game.Multiplier, player.Level * game.Multiplier * baseMultiplier);
        screen.ContinueButton.onClick.AddListener(NextLevel);
    }
    private void NextLevel()
    {
        Bootstrap.Instance.GameRestart(0);
    }
}