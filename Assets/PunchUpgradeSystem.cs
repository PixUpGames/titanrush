using Kuhpik;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchUpgradeSystem : GameSystemWithScreen<TapToScreenUI>
{
    private bool isSpeed;
    private bool isDistance;

    public override void OnInit()
    {
        screen.PowerButton.UpgradeButton.onClick.AddListener(() => UpgradeDistance());
        screen.SpeedButton.UpgradeButton.onClick.AddListener(() => UpgradeSpeed());

        if (player.DistanceUpgradeLevel == 0)
            player.DistanceUpgradeLevel = 1;

        if (player.SpeedUpgradeLevel == 0)
            player.SpeedUpgradeLevel = 1;

        screen.PowerButton.UpdateInfo(player.DistanceUpgradeLevel*100,player.DistanceUpgrade);
        screen.SpeedButton.UpdateInfo(player.SpeedUpgradeLevel * 100, player.SpeedUpgrade);

        if (player.Money < player.DistanceUpgradeLevel * 100)
            screen.PowerButton.SetRewardStatus();

        if (player.Money < player.SpeedUpgradeLevel * 100)
            screen.SpeedButton.SetRewardStatus();
    }

    private void UpgradeSpeed()
    {
        if (isSpeed == false)
        {
            if (player.Money >= player.SpeedUpgradeLevel * 100)
            {
                player.Money -= player.SpeedUpgradeLevel * 100;
            }
        }
        else
        {
            Debug.Log("Rew");
        }
        isSpeed = true;
        player.SpeedUpgradeLevel++;
        player.SpeedUpgrade += 1.2f;

        screen.SpeedButton.UpdateInfo(player.SpeedUpgradeLevel * 100, player.SpeedUpgrade);
        screen.SpeedButton.SetRewardStatus();
    }

    private void UpgradeDistance()
    {
        if (isDistance == false)
        {
            if (player.Money >= player.DistanceUpgradeLevel * 100)
            {
                player.Money -= player.DistanceUpgradeLevel * 100;
            }
        }
        else
        {
            Debug.Log("Rew");
        }
        isDistance = true;
        player.DistanceUpgradeLevel++;
        player.DistanceUpgrade += 1.2f;

        screen.PowerButton.UpdateInfo(player.DistanceUpgradeLevel * 100, player.DistanceUpgrade);
        screen.PowerButton.SetRewardStatus();
    }
}
