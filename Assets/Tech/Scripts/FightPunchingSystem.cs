using Kuhpik;
using Supyrb;
using UnityEngine;

public class FightPunchingSystem : GameSystemWithScreen<FightingScreenUI>
{
    [Header("Final Punch Settings")]
    [SerializeField] private float slowMotionScale = .2f;
    
    [Header("Punch Settings")]
    [SerializeField] private float powerIncreaseOnClick = 1f;
    [SerializeField] private float maxPowerValue = 5f;
    [SerializeField] private float delayBetweenKicks = .5f;
    [SerializeField] private float decreaseMultiplier = 4f;
    [SerializeField] private float playerDamage = 2f;

    private float powerValue = 0;
    public override void OnGameStart()
    {
        Signals.Get<PlayerHitSignal>().AddListener(DoHit);
    }
    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerAnimator.SetFightIdle(true);
    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            powerValue = Mathf.Clamp(powerValue + powerIncreaseOnClick, 0, maxPowerValue);
        }
        else
        {
            powerValue = Mathf.Clamp(powerValue - Time.deltaTime * decreaseMultiplier, 0, maxPowerValue);
        }

        UpdateUI();
        TryPunch();
    }
    private void UpdateUI()
    {
        screen.UpdatePowerSlider(powerValue, maxPowerValue);
    }
    #region Punch
    private void TryPunch()
    {
        if (powerValue <= 0)
        {
            SetPunchAnimation(false);

            return;
        }

        SetPunchAnimation(true);

        TryFinalPunch();
    }

    private void SetPunchAnimation(bool enable)
    {
        game.PlayerComponent.PlayerAnimator.Punch(enable);
    }
    private void TryFinalPunch()
    {
        if (game.enemyBoss.GetHealth() - playerDamage <= 0)
        {
            Time.timeScale = slowMotionScale;
            game.PlayerComponent.PlayerAnimator.ClearAllAnimations();
            game.PlayerComponent.PlayerAnimator.SetFinalKick();

            return;
        }
    }

    public void DoHit()
    {
        /// Change To changeable value
        game.enemyBoss.ReceiveDamage(playerDamage);
    }
    #endregion
}