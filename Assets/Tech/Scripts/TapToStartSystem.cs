using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStartSystem : GameSystemWithScreen<TapToScreenUI>
{
    [SerializeField] private GameObject gateGO;
    [SerializeField] private float gateHeightOffset;
    [SerializeField] private Vector3 touchPos;
    private bool isStarted;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.gameObject.SetActive(false);
        game.Cameras.SetStartCamera();
        screen.ShopButton.onClick.AddListener(() => UIManager.GetUIScreen<ShopUIScreen>().ShopWindow.SetActive(true));
        UIManager.GetUIScreen<GameUIScreen>().UpdateLevelCounter(player.Level);
        UIManager.GetUIScreen<GameUIScreen>().UpdateCoinsCounter(player.Money);
    }
    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
        }

        if (touchPos != Vector3.zero)
        {
            Debug.Log(Vector3.Distance(touchPos, Input.mousePosition));
            if (Vector3.Distance(touchPos, Input.mousePosition) > 400)
            {
                StartGame();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchPos = Vector3.zero;
        }
    }

    private void StartGame()
    {
        if (isStarted == false)
        {
            isStarted = true;

            game.PlayerComponent.RotateModel(game.PlayerComponent.transform.position + Vector3.forward);

            var multiplierController = FindObjectOfType<MultiplierHandlerComponent>();

            if (multiplierController != null)
            {
                var gate = Instantiate(gateGO, multiplierController.GetLastMultiplier(15f).transform.position + new Vector3(0, gateHeightOffset, .4f), Quaternion.identity);
                gate.transform.DORotate(new Vector3(0, 180, 0), 0);
            }

            Bootstrap.Instance.ChangeGameState(GameStateID.Game);
        }
    }
}