using Kuhpik;
using UnityEngine;

public class BossPunchSystem : GameSystem
{
    [SerializeField] private Vector2 randomDelayRange;

    private float delayTime; 

    private EnemyComponent enemy;

    public override void OnStateEnter()
    {
        enemy = FindObjectOfType<EnemyComponent>();
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
        enemy.DoPunch();

        delayTime = Time.time + Random.Range(randomDelayRange.x, randomDelayRange.y);
    }
}