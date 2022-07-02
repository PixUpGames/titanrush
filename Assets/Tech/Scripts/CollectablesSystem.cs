using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectablesSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField, Tag]
    private string collectablesTag;
    [SerializeField] private int coinIncrease = 51;
    [SerializeField] private GameObject pickUpVFX;
    private MutateSignal mutateSignal;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.SetMutationValue(0, game.LevelConfig.MutationBarsToEvolve);
        game.PlayerComponent.PlayerCanvas.SetMutation(0);

        game.PlayerComponent.OnTriggerEnterComp.OnEnter += OnCollectablesPick;

        mutateSignal = Signals.Get<MutateSignal>();
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

                        var go = Instantiate(pickUpVFX, other.position, Quaternion.identity);
                        Destroy(go, 2f);
                        
                        if(game.MutationBars >= game.LevelConfig.MutationBarsToEvolve)
                        {
                            mutateSignal.Dispatch();
                            game.MutationBars = 0;
                        } 

                        break;
                    }
                case Collectable.CUSTOMIZABLE:
                    {
                        var customizable = (CustomizablePickUpComponent) collectable;

                        if (!player.OpenedCustomizables.Contains(customizable.GetItemType))
                        {
                            player.OpenedCustomizables.Add(customizable.GetItemType);

                            Debug.Log($"Added {customizable.GetItemType.GetName()}");
                        }

                        break;
                    }

                default:
                    {
                        throw new System.Exception("Can't get any Collectable Component");

                        break;
                    }
            }
            

            Destroy(other.gameObject);
        }
    }
}