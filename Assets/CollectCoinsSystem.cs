using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class CollectCoinsSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField, Tag]
    private string coinTag;
    [SerializeField] private int coinIncrease = 51;

    public override void OnStateEnter()
    {
        game.playerComponent.OnTriggerEnterComp.OnEnter += OnCollectCoin;
    }
    public override void OnStateExit()
    {
        game.playerComponent.OnTriggerEnterComp.OnEnter -= OnCollectCoin;
    }

    private void OnCollectCoin(Transform other, Transform @object)
    {
        if (other.CompareTag(coinTag))
        {
            game.Coins += coinIncrease;

            screen.UpdateCoinsCounter(game.Coins);

            Destroy(other.gameObject);
        }
    }
}