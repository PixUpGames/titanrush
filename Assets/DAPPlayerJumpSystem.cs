using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class DAPPlayerJumpSystem : GameSystem
{
    [SerializeField] private float jumpHoldTimer = 0.5f;

    private float timer;
    private int currentPointIndex = 0;

    private Vector3 prevPos;

    private HammerFinishComponent finishComponent;

    private bool jumping;

    public override void OnStateEnter()
    {
        finishComponent = (HammerFinishComponent) game.Finish;
    }
    public override void OnUpdate()
    {
        game.PlayerComponent.transform.LookAt(finishComponent.BigTitan.transform);
        
        if (jumping || game.punchAndDodgeState != EnemyState.PUNCH)
        {
            return;
        }

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

                var direction = (int) Mathf.Sign(delta);

                Jump(direction);
            }
        }
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
        game.PlayerComponent.transform.DOMove(finishComponent.GetPoint(currentPointIndex).position, 1f).OnComplete(FinishJump);
    }
    private void FinishJump()
    {
        jumping = false;
    }
}