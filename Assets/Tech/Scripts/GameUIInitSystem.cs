using Kuhpik;
using UnityEngine;

public class GameUIInitSystem : GameSystemWithScreen<GameUIScreen>
{
    public override void OnStateEnter()
    {
        screen.UpdateLevelCounter(player.Level);
        screen.UpdateCoinsCounter(0);
    }
}