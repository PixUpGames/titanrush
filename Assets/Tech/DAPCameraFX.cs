using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAPCameraFX : GameSystem
{
    public override void OnGameStart()
    {
        Signals.Get<PlayerHitSignal>().AddListener(ShakeCamera);
        Signals.Get<EnemyHitSignal>().AddListener(RedCamera);
    }

    private void ShakeCamera()
    {
        game.Cameras.ImpulseCamera();
    }
    private void RedCamera()
    {

    }
}
