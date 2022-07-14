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

    private Vector3 prevMousePos;
    private Vector3 deltaVector;
    private Vector3 targetVector;
    private float remapMultiplyer;
    private float lerpedYEuler;


    private RopeTriggerHolderComponent ropeTrigger;
    private bool isFlying;

    public override void OnInit()
    {
        game.PlayerComponent.StartRunning(true);
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += SetFlyAnim;
        game.PlayerComponent.OnTriggerEnterComp.OnEnter += SetRopesTargets;
        game.playerSpeed = speed;
        remapMultiplyer = RemapMovement();
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
            var deltaMos = Input.mousePosition - prevMousePos;
            targetVector = deltaMos * remapMultiplyer;
            deltaMos.y = 0;
            deltaVector += targetVector;
            targetVector.y = 0;
            prevMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            targetVector = Vector3.zero;
        }

        deltaVector += Vector3.forward;

        MovePlayerOnSides();
        MovePlayerForward();
        RotateModel();

        if (ropeTrigger != null)
        {
            game.PlayerComponent.PlayerAnimator.ConnectRopes(ropeTrigger);
        }
    }

    private float RemapMovement()
    {
        return (2.8f / (Screen.currentResolution.height / 2));
    }

    private void MovePlayerForward()
    {
        game.PlayerComponent.NavMesh.Move(deltaVector * Time.deltaTime * game.playerSpeed);
    }

    private void MovePlayerOnSides()
    {
        if ((game.PlayerComponent.transform.position + (targetVector * sensitivityDivider)).x<1.6f || (game.PlayerComponent.transform.position + (targetVector * sensitivityDivider)).x > -1.6f)
        {
            game.PlayerComponent.NavMesh.Warp(game.PlayerComponent.transform.position + (targetVector * sensitivityDivider));
            var xOffset = Mathf.Clamp(game.PlayerComponent.transform.position.x, -1.6f, 1.6f);
            Vector3 clampedVector = game.PlayerComponent.transform.position;
            clampedVector.x = xOffset;
            game.PlayerComponent.transform.position = clampedVector;
        }
        else
        {
            return;
        }
    }

    private void RotateModel()
    {
        lerpedYEuler = Mathf.Lerp(lerpedYEuler, 20 * targetVector.normalized.x, 8 * Time.deltaTime);

        game.PlayerComponent.CurrentModel.transform.eulerAngles = new Vector3(0, lerpedYEuler, 0);
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