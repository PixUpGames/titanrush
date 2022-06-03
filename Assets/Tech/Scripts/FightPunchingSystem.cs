using Kuhpik;
using UnityEngine;

public class FightPunchingSystem : GameSystemWithScreen<FightingScreenUI>
{
    [SerializeField] private float powerIncreaseOnClick = 1f;
    [SerializeField] private float maxPowerValue = 5f;
    [SerializeField] private float delayBetweenKicks = .5f;
    [SerializeField] private float decreaseMultiplier = 4f;

    private float powerValue = 0;

    private float punchTimer;

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
            return;
        }

        if (punchTimer <= Time.time)
        {
            Punch();
        }
    }
    private void Punch()
    {
        punchTimer = Time.time + delayBetweenKicks;

        game.PlayerComponent.PlayerAnimator.SetKickAnimation();

        /// Change To changeable value
        game.enemyBoss.ReceiveDamage(2f);

        if (game.enemyBoss.GetHealth() <= 0)
        {
            Bootstrap.Instance.ChangeGameState(GameStateID.EnemyDefeated);
        }
    }
    #endregion
}