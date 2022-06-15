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
        SidePlayerMove();
    }
    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            var deltaMos = Input.mousePosition - prevMousePos;
            deltaMos.y = 0;
            deltaVector = deltaMos;

            prevMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            deltaVector = Vector3.zero;
        }
    }

    private void SidePlayerMove()
    {
        game.PlayerComponent.NavMesh.Move(Time.fixedDeltaTime * deltaVector / sensitivityDivider);
    }
    private void MovePlayerForward()
    {
        game.PlayerComponent.NavMesh.Move(Vector3.forward * Time.fixedDeltaTime * game.playerSpeed);
    }
}