using Kuhpik;
using System.Collections;
using UnityEngine;

public class EnemyDefeatSystem : GameSystem
{
    [SerializeField] private float distance = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float singleTileWidth = 0.86f;
    [SerializeField] private GameObject breakableWall;

    private Vector3 startPos;
    private FinishStartComponent finishStart;

    public override void OnStateEnter()
    {
        finishStart = FindObjectOfType<FinishStartComponent>();

        startPos = game.enemyBoss.transform.position;
        game.Cameras.SetDefeatedBossCamera();

        SpawnWalls();

        StartCoroutine(MoveEnemy());
    }

    private void SpawnWalls()
    {
        Instantiate(breakableWall, finishStart.transform.position + Vector3.forward * distance, Quaternion.identity);
    }
    private IEnumerator MoveEnemy()
    {
        while(Vector3.Distance(finishStart.transform.position, game.enemyBoss.transform.position) < distance)
        {
            game.enemyBoss.transform.Translate(-Vector3.forward * Time.deltaTime * speed);

            yield return null;
        }

        game.enemyBoss.DisableAnimator();

        yield return new WaitForSeconds(1f);

        Finish();
    }
    private void Finish()
    {
        Bootstrap.Instance.ChangeGameState(GameStateID.Win);
    }
}