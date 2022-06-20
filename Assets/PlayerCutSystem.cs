using Kuhpik;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
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

            if (cuttable.GetCuttableType() == CuttableType.Hand)
            {
                CutHand();
            }
            else
            {
                CutHead();
            }
        }
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
