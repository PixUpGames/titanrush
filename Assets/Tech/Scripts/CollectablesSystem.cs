using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField, Tag]
    private string collectablesTag;
    [SerializeField] private int coinIncrease = 51;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.SetMutationValue(0, game.LevelConfig.MutationBarsToEvolve);

        game.PlayerComponent.OnTriggerEnterComp.OnEnter += OnCollectablesPick;
    }
    public override void OnStateExit()
    {
        game.PlayerComponent.OnTriggerEnterComp.OnEnter -= OnCollectablesPick;
    }

    private void OnCollectablesPick(Transform other, Transform @object)
    {
        if (other.CompareTag(collectablesTag))
        {
            var collectable = other.GetComponent<CollectableComponent>();

            if (collectable == null)
            {
                return;
            }

            switch (collectable.GetCollectable)
            {
                case Collectable.COIN:
                    {
                        game.Coins += coinIncrease;
                        screen.UpdateCoinsCounter(game.Coins);

                        break;
                    }
                case Collectable.POWER_DOWN:
                    {
                        game.MutationBars = Mathf.Max(0, game.MutationBars - 1);
                        game.PlayerComponent.PlayerCanvas.SetMutationValue(game.MutationBars, game.LevelConfig.MutationBarsToEvolve);

                        break;
                    }
                case Collectable.POWER_UP:
                    {
                        game.MutationBars = Mathf.Min(game.LevelConfig.MutationBarsToEvolve, game.MutationBars + 1);
                        game.PlayerComponent.PlayerCanvas.SetMutationValue(game.MutationBars, game.LevelConfig.MutationBarsToEvolve);

                        break;
                    }

                default:
                    {
                        Debug.LogError("Can't get any Collectable Component");

                        break;
                    }
            }
            

            Destroy(other.gameObject);
        }
    }
}