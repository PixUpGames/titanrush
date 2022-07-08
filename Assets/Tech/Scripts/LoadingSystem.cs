using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class LoadingSystem : GameSystem
{
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
        game.Cameras.SetTargetPlayer(game.PlayerComponent.transform);
    }

    private static void InitGameSettings()
    {
        Signals.Clear();
        Application.targetFrameRate = 80;
    }

    private void BuildNavMeshes()
    {
        var navMeshes = FindObjectsOfType<NavMeshSurface>();

        foreach (var navMesh in navMeshes)
        {
            navMesh.BuildNavMesh();
        }

        if (game.LevelConfig.WithFlying == true)
        {
            GameObject airTempRoad = FindObjectOfType<AirTempRoadComponent>().gameObject;

            if (airTempRoad != null)
                airTempRoad.SetActive(false);
        }
    }

    private void FindObjects()
    {
        game.Cameras = FindObjectOfType<CamerasComponent>();
        game.Finish = FindObjectOfType<FinishComponent>();
        game.enemyBoss = FindObjectOfType<EnemyComponent>();

        var players = FindObjectsOfType<PlayerComponent>(true);

        for (int i = 0; i < players.Length; i++)
        {
            if (game.LevelConfig.PlayerType == players[i].PlayerType)
            {
                players[i].gameObject.SetActive(true);
                game.PlayerComponent = players[i];
            }
            else
            {
                DestroyImmediate(players[i].gameObject);
            }
        }
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