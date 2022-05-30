using Kuhpik;
using UnityEngine;

public class LoadingSystem : GameSystem
{
    /// <summary>
    /// TODO: Change it when Levels will be ready
    /// </summary>
    [SerializeField] private Level debugLevel;
    public override void OnInit()
    {
        game.PlayerComponent = FindObjectOfType<PlayerComponent>();
        game.LevelConfig = debugLevel;
        game.Cameras = FindObjectOfType<CamerasComponent>();
        game.Finish = FindObjectOfType<FinishComponent>();
    }
}