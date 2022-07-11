using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStartSystem : GameSystemWithScreen<TapToScreenUI>
{
    [SerializeField] private GameObject gateGO;
    [SerializeField] private float gateHeightOffset;
    [SerializeField] private int swipeDistance;
    private Vector3 touchPos;
    private bool isStarted;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerCanvas.gameObject.SetActive(false);
        game.Cameras.SetStartCamera();
        screen.ShopButton.onClick.AddListener(() => UIManager.GetUIScreen<ShopUIScreen>().ShopWindow.SetActive(true));
        UIManager.GetUIScreen<GameUIScreen>().UpdateLevelCounter(player.Level);
        UIManager.GetUIScreen<GameUIScreen>().UpdateCoinsCounter(player.Money);

        screen.DeLevel.onClick.AddListener(DecreaseLevel);
        screen.InLevel.onClick.AddListener(IncreaseLevel);
    }
    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
        }

        if (touchPos != Vector3.zero)
        {
            if (Vector3.Distance(touchPos, Input.mousePosition) > swipeDistance)
            {
                StartGame();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchPos = Vector3.zero;
        }

        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartGame();
        //}
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

    private void IncreaseLevel()
    {
        if (player.Level <= 19)
        {
            player.Level++;
        }
        Bootstrap.Instance.GameRestart(0);
    }

    private void DecreaseLevel()
    {
        if (player.Level > 0)
        {
            player.Level--;
        }
        Bootstrap.Instance.GameRestart(0);
    }
}