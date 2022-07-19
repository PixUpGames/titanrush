using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAPCameraFX : GameSystemWithScreen<FightingScreenUI>
{
    public override void OnInit()
    {
        Signals.Get<PlayerHitSignal>().AddListener(ShakeCamera);
        Signals.Get<HammerHitSignal>().AddListener(RedCamera);
    }

    private void ShakeCamera()
    {
        game.Cameras.ImpulseCamera();
    }
    private void RedCamera()
    {
        game.Cameras.ImpulseCamera();
        screen.PunchRedOffset.DOFade(1, 0.5f).OnComplete(() => screen.PunchRedOffset.DOFade(0, 0.5f));
    }
}
