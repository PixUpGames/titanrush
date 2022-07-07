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
    [SerializeField] private int maxLevels;
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

        GameObject airTempRoad = GameObject.FindGameObjectWithTag("AirTempRoad");

        if (airTempRoad != null)
            airTempRoad.SetActive(false);
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
        int levelIndex=0;


        if (debug)
        {
            game.LevelConfig = debugLevel;
        }
        else
        {
            CreateLevel(levelIndex);
        }

        Instantiate(game.LevelConfig.LevelPrefab, Vector3.zero, Quaternion.identity);
    }

    private void CreateLevel(int level)
    {
        level = player.Level;
        game.LevelConfig = Resources.Load<Level>(string.Format(levelsPath, level+1));
    }
}