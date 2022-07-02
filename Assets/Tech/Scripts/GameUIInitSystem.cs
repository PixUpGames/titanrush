using Kuhpik;
using UnityEngine;

public class GameUIInitSystem : GameSystemWithScreen<GameUIScreen>
{
    public override void OnStateEnter()
    {
        game.Cameras.SetMainCamera();
        game.PlayerComponent.PlayerCanvas.gameObject.SetActive(true);
        screen.UpdateLevelCounter(player.Level);
        screen.UpdateCoinsCounter(player.Money);
    }
}