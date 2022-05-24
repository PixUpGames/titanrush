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
        game.playerComponent = FindObjectOfType<PlayerComponent>();
        game.LevelConfig = debugLevel;
    }
}