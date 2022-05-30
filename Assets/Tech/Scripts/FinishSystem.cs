using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class FinishSystem : GameSystem
{
    [SerializeField, Tag]
    private string finishTag;
    public override void OnStateEnter()
    {
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += ChooseFinishState;
    }

    private void ChooseFinishState(Transform other, Transform @object)
    {
        if (other.CompareTag(finishTag))
        {
            Bootstrap.Instance.ChangeGameState(GameStateID.PrepareFighting);

            game.Cameras.SetFightCamera();

            game.PlayerComponent.OnTriggerEnterComp.OnEnter -= ChooseFinishState;
            game.PlayerComponent.PlayerCanvas.gameObject.SetActive(false);
        }
    } 
}