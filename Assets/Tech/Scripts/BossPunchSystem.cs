using Kuhpik;
using UnityEngine;

public class BossPunchSystem : GameSystem
{
    [SerializeField] private Vector2 randomDelayRange;

    private float delayTime; 

    public override void OnStateEnter()
    {
        game.enemyBoss.StartBattle();
    }

    public override void OnUpdate()
    {
        if (delayTime <= Time.time)
        {
            Punch();
        }
    }
    private void Punch()
    {
        game.enemyBoss.DoPunch();
        game.PlayerComponent.ReceiveDamage(1f);

        if (game.PlayerComponent.GetHealth() <= 0)
        {
            Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
        }

        delayTime = Time.time + Random.Range(randomDelayRange.x, randomDelayRange.y);
    }
}