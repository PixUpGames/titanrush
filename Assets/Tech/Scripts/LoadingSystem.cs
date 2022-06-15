using Kuhpik;
using Supyrb;
using UnityEngine;

public class LoadingSystem : GameSystem
{
    /// <summary>
    /// TODO: Change it when Levels will be ready
    /// </summary>
    [SerializeField] private Level debugLevel;
    public override void OnInit()
    {
        Signals.Clear();

        game.PlayerComponent = FindObjectOfType<PlayerComponent>();
        game.LevelConfig = debugLevel;
        game.Cameras = FindObjectOfType<CamerasComponent>();
        game.Finish = FindObjectOfType<FinishComponent>();
        game.enemyBoss = FindObjectOfType<EnemyComponent>();

        game.PlayerComponent.Init();
    }
}