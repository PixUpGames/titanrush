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

        if (game.MutationLevel == 1)
        {
            game.PlayerComponent.PlayerAnimator.CameraHolder.DOLocalMove(new Vector3(0.2f, 0, -0.602f), 0.5f);

        }
        else if (game.MutationLevel==2)
        {
            game.PlayerComponent.PlayerAnimator.CameraHolder.DOLocalMove(new Vector3(1.071f, 1.29f, -1.032f), 0.5f);
        }
        else if (game.MutationLevel == 3)
        {
            game.PlayerComponent.PlayerAnimator.CameraHolder.DOLocalMove(new Vector3(1.059f, 1.195f, -0.986f), 0.5f);
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