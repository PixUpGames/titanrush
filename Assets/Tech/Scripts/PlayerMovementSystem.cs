using Kuhpik;
using System;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float sensitivityDivider = 4f;

    private Vector3 prevMousePos;

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
            var normalizedMosPos = deltaMos;

            game.playerComponent.NavMesh.Move(Time.deltaTime * normalizedMosPos / sensitivityDivider);

            prevMousePos = Input.mousePosition;
        }

        MovePlayerForward();
    }

    private void MovePlayerForward()
    {
        game.playerComponent.NavMesh.Move(Vector3.forward * Time.deltaTime * speed);
    }
}