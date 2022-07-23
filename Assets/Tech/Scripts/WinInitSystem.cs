using Kuhpik;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class WinInitSystem : GameSystemWithScreen<WinUIScreen>
{
    [SerializeField] private float baseMultiplier = 10f;
    [SerializeField] private float multiplySpeed;
    [SerializeField] private float income;
    [SerializeField] private float prize;
    [SerializeField] private List<ShopItemConfig> skinConfigs = new List<ShopItemConfig>();
    public override void OnInit()
    {
        income = (player.Level + 1) * game.Multiplier * baseMultiplier;
        screen.InitScreen(game.Multiplier, income);
        screen.ContinueButton.onClick.AddListener(NextLevel);
        screen.PrizeButton.onClick.AddListener(GetPrize);
        screen.RewardSkinButton.onClick.AddListener(GetSkinByReward);
        player.Level++;
        player.Money += income;
        screen.CoinsText.text = income.ToString("0");
        StartMultiplyBar();

        SetTargetSkin();
        SetTargetSkinProgess();

        StartCoroutine(ContinueTimer());
    }

    private IEnumerator ContinueTimer()
    {
        screen.ContinueButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        screen.ContinueButton.gameObject.SetActive(true);
        screen.ContinueButton.transform.DOScale(Vector3.one, 0.3f).OnComplete(() => screen.ContinueButton.transform.DOPunchScale(Vector3.one * 0.15f, 0.3f, 3, 3));
    }

    private void StartMultiplyBar()
    {
        if (screen.MultiplyBar.value == screen.MultiplyBar.minValue)
        {
            screen.MultiplyBar.DOValue(screen.MultiplyBar.maxValue, multiplySpeed).SetEase(Ease.Linear).OnComplete(() => StartMultiplyBar()).OnUpdate(() => CheckMultiplyBar(screen.MultiplyBar.value));
        }
        else
        {
            screen.MultiplyBar.DOValue(screen.MultiplyBar.minValue, multiplySpeed).SetEase(Ease.Linear).OnComplete(() => StartMultiplyBar()).OnUpdate(() => CheckMultiplyBar(screen.MultiplyBar.value));
        }
    }

    private void CheckMultiplyBar(float value)
    {
        if (value < 0.150)
        {
            UpdateMultiplyText(2);
        }
        else if (value > 0.150 && value < 0.350)
        {
            UpdateMultiplyText(3);
        }
        else if (value > 0.350 && value < 0.620)
        {
            UpdateMultiplyText(5);
        }
        else if (value > 0.620 && value < 0.830)
        {
            UpdateMultiplyText(3);
        }
        else if (value > 0.830 && value <= 1)
        {
            UpdateMultiplyText(2);
        }
    }

    private void UpdateMultiplyText(int multiply)
    {
        screen.MultiplyText.text = (income * multiply).ToString("0");
        prize = income * multiply;
    }

    private void GetPrize()
    {
        screen.MultiplyBar.DOKill();
        player.Money += Mathf.RoundToInt(prize);
        Debug.LogError("[REWARD] multiply coins");
        NextLevel();
    }

    private void NextLevel()
    {
        Bootstrap.Instance.SaveGame();
        Bootstrap.Instance.GameRestart(0);
    }

    private void SetTargetSkin()
    {
        if (player.targetSkinIndex < 4)
        {
            player.targetSkin = skinConfigs[player.targetSkinIndex].CustomizableType;
            screen.ShadowImage.sprite = skinConfigs[player.targetSkinIndex].Icon;
            screen.SkinImage.sprite = skinConfigs[player.targetSkinIndex].Icon;
            screen.ShadowImage.fillAmount = 1 - player.skinProgress;
        }
    }

    private void SetTargetSkinProgess()
    {
        screen.ShadowImage.DOFillAmount(screen.ShadowImage.fillAmount - 0.25f, 1f).OnComplete(CheckSkinProgres);
        player.skinProgress += 0.25f;
    }

    private void CheckSkinProgres()
    {
        if (screen.ShadowImage.fillAmount == 0)
        {
            GetTargetSkin();
        }
    }

    private void GetTargetSkin()
    {
        screen.SkinParticle.gameObject.SetActive(true);
        player.OpenedCustomizables.Add(skinConfigs[player.targetSkinIndex].CustomizableType);
        //player.skinType = skinConfigs[player.targetSkinIndex].CustomizableType;
        player.skinProgress = 0f;

        if (player.targetSkinIndex < 4)
            player.targetSkinIndex++;
    }

    private void GetSkinByReward()
    {
        screen.ShadowImage.DOFillAmount(0, 1f).OnComplete(() => { GetTargetSkin(); Debug.Log("[REWARD] SKIN"); });
    }
}