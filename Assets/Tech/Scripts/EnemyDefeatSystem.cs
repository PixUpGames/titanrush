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
    [SerializeField] private GameObject breakableWall;

    private Vector3 startPos;
    private FinishStartComponent finishStart;

    public override void OnStateEnter()
    {
        finishStart = FindObjectOfType<FinishStartComponent>();

        startPos = game.enemyBoss.transform.position;
        game.Cameras.SetDefeatedBossCamera(game.enemyBoss.transform);
        game.enemyBoss.FlyAway();

        SpawnWalls();

        StartCoroutine(MoveEnemy());
    }

    private void SpawnWalls()
    {
        GameObject fistWall=Instantiate(breakableWall, finishStart.transform.position + Vector3.forward * distance, Quaternion.identity);
        fistWall.transform.DOScale(Vector3.one * game.MutationLevel, 0.3f);
        GameObject secondWall= Instantiate(breakableWall, finishStart.transform.position + Vector3.forward * distance / 2, Quaternion.identity);
        secondWall.transform.DOScale(Vector3.one * game.MutationLevel, 0.3f);
    }
    private IEnumerator MoveEnemy()
    {
        game.enemyBoss.StopAnimator();

        while(Vector3.Distance(finishStart.transform.position, game.enemyBoss.transform.position) < distance)
        {
            game.enemyBoss.transform.Translate(-Vector3.forward * Time.deltaTime * speed);

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