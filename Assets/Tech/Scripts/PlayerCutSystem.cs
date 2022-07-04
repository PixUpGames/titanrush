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
            CollectableComponent coin = Instantiate(coinPrefab,other.position,coinPrefab.transform.rotation);
            coin.transform.DOJump(game.PlayerComponent.PlayerAnimator.Head.transform.position,3f,1,1f).OnUpdate(()=>coin.transform.DOMove(game.PlayerComponent.PlayerAnimator.Head.transform.position, 0.3f));
            //coin.transform.DOScale(Vector3.one / 3, 1f);
        }
    }
}