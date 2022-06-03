using Kuhpik;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStartSystem : GameSystemWithScreen<TapToScreenUI>
{
    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.gameObject.SetActive(false);

        UIManager.GetUIScreen<GameUIScreen>().UpdateLevelCounter(player.Level);
        UIManager.GetUIScreen<GameUIScreen>().UpdateCoinsCounter(0);
    }
    public override void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }
    private void StartGame()
    {
        game.PlayerComponent.RotateModel(game.PlayerComponent.transform.position + Vector3.forward);

        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
    }
}