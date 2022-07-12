using DG.Tweening;
using Kuhpik;
using System.Collections;
using UnityEngine;

public class EnemyDefeatSystem : GameSystem
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float forceAfterFinish = 40f;
    [SerializeField] private float singleTileWidth = 0.86f;
    [SerializeField] private GameObject[] breakableWalls;
    [SerializeField] private GameObject finalWall;

    private Vector3 startPos;
    private FinishStartComponent finishStart;

    public override void OnInit()
    {
        finishStart = FindObjectOfType<FinishStartComponent>();

        startPos = game.enemyBoss.transform.position;
        game.Cameras.SetDefeatedBossCamera(game.enemyBoss.CameraHolder);
        game.enemyBoss.FlyAway();

        SpawnWalls();

        StartCoroutine(MoveEnemy());
    }

    private void SpawnWalls()
    {
        //GameObject fistWall = Instantiate(breakableWalls[0], finishStart.transform.position + Vector3.forward * (distance + player.DistanceUpgrade), Quaternion.identity);
        //fistWall.transform.DOScale(Vector3.one * game.MutationLevel * 2, 0.3f);
        //GameObject secondWall = Instantiate(breakableWalls[0], finishStart.transform.position + Vector3.forward * (distance + player.DistanceUpgrade) / 2, Quaternion.identity);
        //secondWall.transform.DOScale(Vector3.one * game.MutationLevel*2, 0.3f);


        //for (int i = 0; i < breakableWalls.Length; i++)
        //{
        //    breakableWalls[i].transform.position = finishStart.transform.position + Vector3.forward * (distance + player.DistanceUpgrade)/(i+1);
        //    //breakableWalls[i].transform.DOScale(Vector3.one * game.MutationLevel, 0.3f);
        //}

        finalWall.transform.position = finishStart.transform.position + Vector3.forward * (distance + player.DistanceUpgrade) + (Vector3.up*1.5f);
        finalWall.transform.position += (Vector3.forward*1.3f);
    }

    private IEnumerator MoveEnemy()
    {
        game.enemyBoss.StopAnimator();

        while(Vector3.Distance(finishStart.transform.position, game.enemyBoss.transform.position) < (distance + player.DistanceUpgrade))
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

        Finish();
    }

    private void Finish()
    {
        Bootstrap.Instance.ChangeGameState(GameStateID.Win);
    }
}