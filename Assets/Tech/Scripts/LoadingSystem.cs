using Kuhpik;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class LoadingSystem : GameSystem
{
    /// <summary>
    /// TODO: Change it when Levels will be ready
    /// </summary>
    [SerializeField] private string levelsPath;

    private List<Level> levelConfigs = new List<Level>();
    public override void OnInit()
    {
        Signals.Clear();

        game.PlayerComponent = FindObjectOfType<PlayerComponent>();
        levelConfigs = Resources.LoadAll<Level>(levelsPath).ToList();

        levelConfigs.OrderBy(x => x.index);

        game.LevelConfig = levelConfigs[player.Level - 1];
        Instantiate(game.LevelConfig.LevelPrefab, Vector3.zero, Quaternion.identity);

        var navMeshes = FindObjectsOfType<NavMeshSurface>();

        foreach (var navMesh in navMeshes)
        {
            navMesh.BuildNavMesh();
        }

        game.Cameras = FindObjectOfType<CamerasComponent>();
        game.Finish = FindObjectOfType<FinishComponent>();
        game.enemyBoss = FindObjectOfType<EnemyComponent>();

        game.PlayerComponent.Init();
    }
}