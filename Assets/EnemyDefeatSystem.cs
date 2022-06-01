using Kuhpik;
using System.Collections;
using UnityEngine;

public class EnemyDefeatSystem : GameSystem
{
    [SerializeField] private float distance = 10f;
    [SerializeField] private float speed = 10f;

    private Vector3 startPos;

    public override void OnStateEnter()
    {
        startPos = game.enemyBoss.transform.position;
        game.Cameras.SetDefeatedBossCamera();

        StartCoroutine(MoveEnemy());
    }
    private IEnumerator MoveEnemy()
    {
        while(Vector3.Distance(startPos, game.enemyBoss.transform.position) < distance)
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