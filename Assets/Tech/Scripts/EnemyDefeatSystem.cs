using DG.Tweening;
using Kuhpik;
using System.Collections;
using UnityEngine;

public class EnemyDefeatSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float forceAfterFinish = 40f;
    [SerializeField] private float singleTileWidth = 0.86f;
    [SerializeField] private GameObject[] breakableWalls;
    [SerializeField] private GameObject finalWall;
    [SerializeField] private ShopItemConfig[] hatsConfigs;

    private Vector3 startPos;
    private FinishStartComponent finishStart;

    public override void OnInit()
    {
        finishStart = FindObjectOfType<FinishStartComponent>();

        startPos = game.enemyBoss.transform.position;
        game.Cameras.SetDefeatedBossCamera(game.enemyBoss.CameraHolder);
        //game.enemyBoss.FlyAway();

        SpawnWalls();

        StartCoroutine(MoveEnemy());

        if (player.TempItem != CustomizableType.Null)
            InitTempItemScreen();
    }

    private void InitTempItemScreen()
    {
        foreach (var item in hatsConfigs)
        {
            if (item.CustomizableType == player.TempItem)
            {
                screen.TempItemIcon.sprite = item.Icon;
            }
        }

        screen.GetItemButton.onClick.AddListener(GetItem);
        screen.LoseItemButton.onClick.AddListener(LoseItem);
    }

    private void SpawnWalls()
    {
        finalWall.transform.position = finishStart.transform.position + Vector3.forward * (distance + player.DistanceUpgrade) + (Vector3.up*2.15f);
        finalWall.transform.position += (Vector3.forward*1.3f);
    }

    private IEnumerator MoveEnemy()
    {
        game.enemyBoss.FlyAway();
        game.enemyBoss.StopAnimator();

        while (Vector3.Distance(finishStart.transform.position, game.enemyBoss.transform.position) < (distance + player.DistanceUpgrade))
        {
            game.enemyBoss.transform.Translate(-Vector3.forward * Time.deltaTime * (speed+player.SpeedUpgrade));
            yield return null;
        }

        game.enemyBoss.DisableAnimator();

        yield return null;

        var ragdoll = Physics.OverlapSphere(game.enemyBoss.transform.position, 3f, enemyLayer);
        foreach(var bone in ragdoll)
        {
            bone.attachedRigidbody.AddForce(Vector3.forward * forceAfterFinish, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(1f);

        if (player.TempItem != CustomizableType.Null)
        {
            screen.TempItemScreen.SetActive(true);
            screen.TempItemScreen.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.Linear);
        }
        else
        {
            Finish();
        }
    }

    private void Finish()
    {
        Bootstrap.Instance.ChangeGameState(GameStateID.Win);
    }

    private void GetItem()
    {
        if (player.TempItem != CustomizableType.Null)
        {
            if (!player.OpenedCustomizables.Contains(player.TempItem))
            {
                player.OpenedCustomizables.Add(player.TempItem);
                player.hatType = player.TempItem;
            }
        }
        player.TempItem = CustomizableType.Null;
        Finish();
    }

    private void LoseItem()
    {
        player.TempItem = CustomizableType.Null;
        Finish();
    }
}