using Kuhpik;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyIncomeSystem : GameSystemWithScreen<OfflineIncomeUIScreen>
{
    public float msToWait;
    private ulong lastTimeClicked;
    private int dailyReward;

    public override void OnInit()
    {
        screen.RewardButton.onClick.AddListener(GetIncome);

        if (PlayerPrefs.HasKey("LastTimeClicked"))
        {
            lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
        }
        else
        {
            lastTimeClicked = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
        }

        if (!Ready())
            screen.IncomeScreen.gameObject.SetActive(false);

            UpdateDailyRewardValues();
    }


    public override void OnUpdate()
    {
        if (screen.IncomeScreen.activeInHierarchy==false)
        {
            if (Ready())
            {
                screen.IncomeScreen.gameObject.SetActive(true);

                screen.TimerText.text = "Ready!";
                return;
            }
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            r += ((int)secondsLeft / 3600).ToString() + "h";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            r += (secondsLeft % 60).ToString("00") + "s";
            screen.TimerText.text = r;
        }
    }

    public void GetIncome()
    {
        player.PlayerDailyRewardCount++;
        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
        player.Money += dailyReward;
        UIManager.GetUIScreen<GameUIScreen>().UpdateCoinsCounter(player.Money);
        screen.IncomeScreen.gameObject.SetActive(false);
        Bootstrap.Instance.SaveGame();

        if (player.PlayerDailyRewardCount == 1)
        {
            Bootstrap.Instance.GetSystem<PunchUpgradeSystem>().ReNewIcons();
        }
    }

    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            screen.IncomeScreen.SetActive(true);
            return true;
        }

        return false;
    }

    private void UpdateDailyRewardValues()
    {
        if (player.PlayerDailyRewardCount == 0)
        {
            screen.IncomeScreen.gameObject.SetActive(true);
            dailyReward = (player.PlayerDailyRewardCount + 1) * 200;
            screen.DailyRewardCount.text = dailyReward.ToString();
        }
        else
        {
            dailyReward = player.PlayerDailyRewardCount * 200;
            screen.DailyRewardCount.text = dailyReward.ToString();
        }
    }
}
