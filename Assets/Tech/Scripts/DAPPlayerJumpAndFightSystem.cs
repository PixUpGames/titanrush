using DG.Tweening;
using Kuhpik;
using Supyrb;
using UnityEngine;

public class DAPPlayerJumpAndFightSystem : GameSystemWithScreen<FightingScreenUI>
{
    [SerializeField] private float jumpHoldTimer = 0.5f;
    private float timer;
    private int currentPointIndex = 0;
    private Vector3 prevPos;
    private HammerFinishComponent finishComponent;
    private bool jumping;

    [Header("Final Punch Settings")]
    [SerializeField] private float slowMotionScale = .2f;
    [Header("Punch Settings")]
    [SerializeField] private float powerIncreaseOnClick = 1f;
    [SerializeField] private float maxPowerValue = 5f;
    [SerializeField] private float delayBetweenKicks = .5f;
    [SerializeField] private float decreaseMultiplier = 4f;
    [SerializeField] private float playerDamage = 2f;

    private float powerValue = 0;

    public override void OnStateEnter()
    {
        game.PlayerComponent.PlayerAnimator.SetFightIdle(true);
    }

    public override void OnInit()
    {
        Signals.Get<PlayerHitSignal>().AddListener(DoHit);
        Signals.Get<HammerHitSignal>().AddListener(HitPlayerByHammer);
        finishComponent = (HammerFinishComponent) game.Finish;
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

        if(game.punchAndDodgeState==EnemyState.DEATH)
            SetPunchAnimation(false);

        TryToFight();
        TryToJump();
        TryToFinalFootKicks();
    }

    private void TryToFinalFootKicks()
    {
        if (jumping || game.punchAndDodgeState != EnemyState.DEATH)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            game.PlayerComponent.PlayerAnimator.SetKickAnimation();
        }
    }

    private void TryToJump()
    {
        game.PlayerComponent.transform.LookAt(finishComponent.BigTitan.transform);

        if (jumping || game.punchAndDodgeState != EnemyState.PUNCH)
        {
            return;
        }

        SetPunchAnimation(false);

        if (Input.GetMouseButtonDown(0))
        {
            timer = Time.time + jumpHoldTimer;

            prevPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (timer < Time.time)
            {
                var delta = Input.mousePosition.x - prevPos.x;

                if (delta == 0 || prevPos == Vector3.zero)
                {
                    return;
                }

                var direction = (int)Mathf.Sign(delta);
                Jump(direction);
            }
        }
    }

    private void TryToFight()
    {
        if (jumping || game.punchAndDodgeState != EnemyState.STUNNED)
        {
            return;
        }

        TryFinalFightState();
    }

    private void TryFinalFightState()
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
        if (game.enemyBoss.GetHealth()<= 0)
        {
            game.PlayerComponent.PlayerAnimator.ClearAllAnimations();

            return;
        }
    }

    public void DoHit()
    {
        /// Change To changeable value
        /// 
        game.enemyBoss.ReceiveDamage(playerDamage);
    }

    public void HitPlayerByHammer()
    {
        game.PlayerComponent.ReceiveDamage(1);
    }

    private void Jump(int deltaIndex)
    {
        jumping = true;

        if (currentPointIndex + deltaIndex < 0)
        {
            currentPointIndex = finishComponent.GetPointsLength() + deltaIndex;
        }
        else if (currentPointIndex + deltaIndex >= finishComponent.GetPointsLength())
        {
            currentPointIndex = 0;
        }
        else
        {
            currentPointIndex += deltaIndex;
        }

        prevPos = Vector3.zero;
        game.PlayerComponent.PlayerAnimator.Jump();
        game.PlayerComponent.transform.DOMove(finishComponent.GetPoint(currentPointIndex).position, 1f).OnComplete(FinishJump);
    }

    private void FinishJump()
    {
        jumping = false;
    }
}