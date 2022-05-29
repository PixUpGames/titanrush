using Kuhpik;
using UnityEngine;

public class GameUIInitSystem : GameSystemWithScreen<GameUIScreen>
{
    public override void OnStateEnter()
    {
        game.Cameras.SetMainCamera();

        screen.UpdateLevelCounter(player.Level);
        screen.UpdateCoinsCounter(0);
    }
}