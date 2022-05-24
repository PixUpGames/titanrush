using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : GameSystem
{
    [SerializeField, Tag]
    private string finishTag;
    public override void OnStateEnter()
    {
        game.playerComponent.OnTriggerEnterComp.OnEnter += ChooseFinishState;
    }

    private void ChooseFinishState(Transform other, Transform @object)
    {
        if (other.CompareTag(finishTag))
        {
            switch (game.LevelConfig.FinishState)
            {
                case FinishState.FIGHTING:
                    {
                        Bootstrap.Instance.ChangeGameState(GameStateID.Fighting);
                        break;
                    }
                case FinishState.DODGE_AND_PUNCH:
                    {
                        Bootstrap.Instance.ChangeGameState(GameStateID.DodgeAndPunch);
                        break;
                    }
            }

            game.playerComponent.OnTriggerEnterComp.OnEnter -= ChooseFinishState;
        }
    } 
}

public enum FinishState
{
    DODGE_AND_PUNCH = 1,
    FIGHTING = 2
}