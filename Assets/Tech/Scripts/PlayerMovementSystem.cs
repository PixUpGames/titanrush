using Kuhpik;
using System;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float sensitivityDivider = 1f;

    private Vector3 prevMousePos;

    private Vector3 deltaVector;
    private Vector3 targetVector;

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
            targetVector = Vector3.zero;
        }
        else if (Input.GetMouseButton(0))
        {
            var deltaMos = Input.mousePosition - prevMousePos;
            deltaMos.y = 0;
            targetVector = Vector3.Lerp(targetVector, deltaMos, Time.deltaTime * sensitivityDivider);
            deltaVector += targetVector;
            prevMousePos = Input.mousePosition;
        }

        deltaVector += Vector3.forward;
    }

    private void MovePlayerForward()
    {
        game.PlayerComponent.NavMesh.Move(deltaVector * Time.fixedDeltaTime * game.playerSpeed);
    }
}