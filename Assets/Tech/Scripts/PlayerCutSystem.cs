using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

public class PlayerCutSystem : GameSystem
{
    [SerializeField, Tag] private string cuttableTag;
    [SerializeField] private CollectableComponent coinPrefab;

    public override void OnStateEnter()
    {
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += ChooseCuttable;
    }

    private void ChooseCuttable(Transform other, Transform @object)
    {
        if (other.CompareTag(cuttableTag))
        {
            var cuttable = other.GetComponent<CuttableItem>();
            SpawnCoins(other);
            cuttable.Cut();
            game.Coins += cuttable.ReceiveAward();

            switch (cuttable.GetCuttableType())
            {
                case CuttableType.Hand:
                    {
                        CutHand();

                        break;
                    }
                case CuttableType.Head:
                    {
                        CutHead();

                        break;
                    }
                case CuttableType.Silly_Titan:
                    {
                        CutSillyTitan();

                        break;
                    }
                case CuttableType.Cannon:
                    {
                        CutSillyTitan();

                        break;
                    }
                default:
                    {
                        throw new Exception("No Types for this cuttable");
                    }
            }
        }
    }

    private void CutSillyTitan()
    {
        game.PlayerComponent.CutHand();
    }

    private void CutHead()
    {
        game.PlayerComponent.CutHead();
    }

    private void CutHand()
    {
        game.PlayerComponent.CutHand();
    }

    private void SpawnCoins(Transform other)
    {
        for (int i = 0; i < 3; i++)
        {
            CollectableComponent coin = Instantiate(coinPrefab,game.PlayerComponent.PlayerAnimator.CameraHolder.transform.position,coinPrefab.transform.rotation);
            coin.transform.DOMove(coin.transform.position + Vector3.one * (2 * i), i + 1f).OnComplete(() => SendCoinToPlayer(coin));
        }
    }

    private void SendCoinToPlayer(CollectableComponent coin)
    {
        coin.transform.DOMove(game.PlayerComponent.PlayerAnimator.CameraHolder.transform.position+Vector3.up*1.1f, 0.2f).OnComplete(()=> {coin.Collider.enabled=true ; coin.transform.position = game.PlayerComponent.transform.position; });
    }
}