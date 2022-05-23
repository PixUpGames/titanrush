using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSystem : GameSystem
{
    public override void OnInit()
    {
        game.playerComponent = FindObjectOfType<PlayerComponent>();
    }
}