using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStartSystem : GameSystemWithScreen<TapToScreenUI>
{
    [SerializeField] private GameObject gateGO;
    [SerializeField] private float gateHeightOffset;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.gameObject.SetActive(false);
        game.Cameras.SetStartCamera();

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

        var multiplierController = FindObjectOfType<MultiplierHandlerComponent>();

        var gate = Instantiate(gateGO, multiplierController.GetLastMultiplier(8f).transform.position + new Vector3(0, gateHeightOffset, .4f), Quaternion.identity);
        gate.transform.DORotate(new Vector3(0, 180, 0), 0);

        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
    }
}