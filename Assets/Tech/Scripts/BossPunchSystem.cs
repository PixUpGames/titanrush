using Kuhpik;
using Supyrb;
using UnityEngine;

public class BossPunchSystem : GameSystem
{
    [SerializeField] private Vector2 randomDelayRange;

    private float delayTime;

    public override void OnGameStart()
    {
        Signals.Get<EnemyHitSignal>().AddListener(DoPunch);
    }
    public override void OnStateEnter()
    {
        game.enemyBoss.Prepare();
        delayTime = Time.time + Random.Range(randomDelayRange.x, randomDelayRange.y);
    }

    public override void OnUpdate()
    {
        if (delayTime <= Time.time)
        {
            TryFinalPunch();
        }
    }
    private void TryFinalPunch()
    {
        if (game.PlayerComponent.GetHealth() <= 0)
        {
            Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
        }

        game.enemyBoss.DoPunch();
        DoPunch();
        delayTime = Time.time + Random.Range(randomDelayRange.x, randomDelayRange.y);
    }
    private void DoPunch()
    {
        game.PlayerComponent.ReceiveDamage(1f);
    }
}