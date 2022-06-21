using Kuhpik;
using NaughtyAttributes;
using System;
using UnityEngine;

public class PlayerCutSystem : GameSystem
{
    [SerializeField, Tag] private string cuttableTag;

    public override void OnStateEnter()
    {
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += ChooseCuttable;
    }
    private void ChooseCuttable(Transform other, Transform @object)
    {
        if (other.CompareTag(cuttableTag))
        {
            var cuttable = other.GetComponent<CuttableItem>();

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
}