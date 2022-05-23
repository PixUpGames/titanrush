using Kuhpik;
using System;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    [SerializeField] private float speed = 4f;

    private Vector3 startingMousePos;

    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {

        }

        MovePlayerForward();
    }

    private void MovePlayerForward()
    {
        game.playerComponent.NavMesh.Move(Vector3.forward * Time.deltaTime * speed);
    }
}