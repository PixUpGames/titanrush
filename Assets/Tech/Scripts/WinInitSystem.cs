using Kuhpik;
using UnityEngine;

public class WinInitSystem : GameSystemWithScreen<WinUIScreen>
{
    [SerializeField] private float baseMultiplier = 10f;
    public override void OnInit()
    {
        float coinsValue = player.Level * game.Multiplier * baseMultiplier;
        screen.InitScreen(game.Multiplier, coinsValue);
        screen.ContinueButton.onClick.AddListener(NextLevel);
        player.Level++;
    }

    private void NextLevel()
    {
        Bootstrap.Instance.GameRestart(0);
    }
}