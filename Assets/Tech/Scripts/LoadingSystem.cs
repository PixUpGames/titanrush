using Kuhpik;
using NaughtyAttributes;
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
    [SerializeField] private bool debug = false;
    [ShowIf("debug"), SerializeField] private Level debugLevel;

    private List<Level> levelConfigs = new List<Level>();
    public override void OnInit()
    {
        InitGameSettings();

        CreateLevel();

        BuildNavMeshes();

        FindObjects();

        game.PlayerComponent.Init();
    }

    private static void InitGameSettings()
    {
        Signals.Clear();

        Application.targetFrameRate = 60;
    }

    private static void BuildNavMeshes()
    {
        var navMeshes = FindObjectsOfType<NavMeshSurface>();

        foreach (var navMesh in navMeshes)
        {
            navMesh.BuildNavMesh();
        }
    }

    private void FindObjects()
    {
        game.Cameras = FindObjectOfType<CamerasComponent>();
        game.Finish = FindObjectOfType<FinishComponent>();
        game.enemyBoss = FindObjectOfType<EnemyComponent>();
        game.PlayerComponent = FindObjectOfType<PlayerComponent>();
    }

    private void CreateLevel()
    {
        if (debug)
        {
            game.LevelConfig = debugLevel;
        }
        else
        {
            levelConfigs = Resources.LoadAll<Level>(levelsPath).ToList();
            levelConfigs.OrderBy(x => x.index);
            game.LevelConfig = levelConfigs[player.Level - 1];
        }

        Instantiate(game.LevelConfig.LevelPrefab, Vector3.zero, Quaternion.identity);
    }
}