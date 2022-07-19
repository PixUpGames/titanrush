using DG.Tweening;
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
    [SerializeField] private GameObject breakWall;
    [ShowIf("debug"), SerializeField] private Level debugLevel;

    private List<Level> levelConfigs = new List<Level>();

    private FinishStartComponent finishStart;
    public override void OnInit()
    {
        InitGameSettings();

        CreateLevel();

        BuildNavMeshes();

        FindObjects();

        SpawnFinishWalls();

        game.PlayerComponent.Init();
        game.Cameras.SetTargetPlayer(game.PlayerComponent.transform);

        TryToRevive();
    }

    private static void InitGameSettings()
    {
        Signals.Clear();
        Application.targetFrameRate = 300;
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
        if (player.Level < maxLevels)
        {
            level = player.Level;
        }
        else
        {
            level = Random.Range(2, 20);
        }

        game.LevelConfig = Resources.Load<Level>(string.Format(levelsPath, level+1));
    }

    private void SpawnFinishWalls()
    {
        if (game.LevelConfig.FinishState == FinishState.FIGHTING)
        {
            finishStart = FindObjectOfType<FinishStartComponent>();
            GameObject fistWall = Instantiate(breakWall, finishStart.transform.position + Vector3.forward * (20 + player.DistanceUpgrade), breakWall.transform.rotation);
            fistWall.transform.DOScale(Vector3.one*game.LevelConfig.WallsScale, 0.3f);
            GameObject secondWall = Instantiate(breakWall, finishStart.transform.position + Vector3.forward * (20 + player.DistanceUpgrade) / 2, breakWall.transform.rotation);
            secondWall.transform.DOScale(Vector3.one * game.LevelConfig.WallsScale, 0.3f);
        }
    }

    private void TryToRevive()
    {
        if (player.isRevive)
        {
            game.PlayerComponent.NavMesh.Warp(player.RevivePos);
            player.isRevive = false;
        }
    }
}