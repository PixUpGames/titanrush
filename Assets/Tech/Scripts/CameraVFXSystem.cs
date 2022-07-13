using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVFXSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<PlayerHitSignal>().AddListener(ShakeCamera);
        Signals.Get<EnemyHitSignal>().AddListener(RedCamera);

        if (game.MutationLevel == 3)
        {
            //game.PlayerComponent.PlayerAnimator.CameraHolder.transform.localPosition = new Vector3(1.059f, 1.195f, -0.986f);
            game.PlayerComponent.PlayerAnimator.CameraHolder.DOLocalMove(new Vector3(1.059f, 1.195f, -0.986f), 0.5f);
            Debug.Log("New Pos");
        }
    }

    private void ShakeCamera()
    {
        game.Cameras.ImpulseCamera();
    }
    private void RedCamera()
    {

    }
}