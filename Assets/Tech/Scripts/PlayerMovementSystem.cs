using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using System;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float sensitivityDivider = 1f;
    [SerializeField, Tag] private string airTag;
    private bool isFlying;
    private Vector3 prevMousePos;

    private Vector3 deltaVector;
    private Vector3 targetVector;

    private RopeTriggerHolderComponent ropeTrigger;

    public override void OnStateEnter()
    {
        game.PlayerComponent.StartRunning(true);
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += SetFlyAnim;
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += SetRopesTargets;
        game.playerSpeed = speed;
    }

    public override void OnFixedUpdate()
    {
        MovePlayerForward();
    }
    public override void OnUpdate()
    {
        deltaVector = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            targetVector = Vector3.zero;
            var deltaMos = Input.mousePosition - prevMousePos;
            deltaMos.y = 0;
            targetVector = Vector3.Lerp(targetVector, deltaMos, Time.deltaTime * sensitivityDivider);
            deltaVector += targetVector;
            prevMousePos = Input.mousePosition;
        }

        deltaVector += Vector3.forward;

        if (isFlying)
        {
            if(ropeTrigger!=null)
                game.PlayerComponent.PlayerAnimator.ConnectRopes(ropeTrigger);
        }
    }

    private void MovePlayerForward()
    {
        game.PlayerComponent.NavMesh.Move(deltaVector * Time.fixedDeltaTime * game.playerSpeed);
    }

    private void SetFlyAnim(Transform other,Transform t2)
    {
        if (other.CompareTag(airTag))
        {
            if (isFlying == false)
            {
                game.PlayerComponent.PlayerAnimator.SetRunAnimation(false);
                game.PlayerComponent.PlayerAnimator.SetFlyAnimation(true);
                game.PlayerComponent.PlayerAnimator.ToggleRopes(true);
                isFlying = true;
            }
            else
            {
                game.PlayerComponent.PlayerAnimator.SetRunAnimation(true);
                game.PlayerComponent.PlayerAnimator.SetFlyAnimation(false);
                game.PlayerComponent.PlayerAnimator.ToggleRopes(false);
                ropeTrigger = null;
                isFlying = false;
            }
        }
    }

    private void SetRopesTargets(Transform other, Transform t2)
    {
        if(other.TryGetComponent(out RopeTriggerHolderComponent ropeTriggerHolder))
        {
            ropeTrigger = ropeTriggerHolder;
        }
    }
}