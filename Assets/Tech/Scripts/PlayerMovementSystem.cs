using Kuhpik;
using System;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float sensitivityDivider = 1f;

    private Vector3 prevMousePos;

    private Vector3 deltaVector;

    public override void OnStateEnter()
    {
        game.PlayerComponent.StartRunning(true);
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
            var deltaMos = Input.mousePosition - prevMousePos;
            deltaMos.y = 0;
            deltaVector += deltaMos / sensitivityDivider;

            prevMousePos = Input.mousePosition;
        }

        deltaVector += Vector3.forward;
    }

    private void MovePlayerForward()
    {
        game.PlayerComponent.NavMesh.Move(deltaVector * Time.fixedDeltaTime * game.playerSpeed);
    }
}