using Kuhpik;
using UnityEngine;
using DG.Tweening;

public class WinInitSystem : GameSystemWithScreen<WinUIScreen>
{
    [SerializeField] private float baseMultiplier = 10f;
    [SerializeField] private float multiplySpeed;
    [SerializeField] private float income;
    public override void OnInit()
    {
        income = (player.Level+1) * game.Multiplier * baseMultiplier;
        screen.InitScreen(game.Multiplier, income);
        screen.ContinueButton.onClick.AddListener(NextLevel);
        screen.PrizeButton.onClick.AddListener(GetPrize);
        player.Level++;
        player.Money += income;
        screen.CoinsText.text = income.ToString();
        StartMultiplyBar();
    }

    private void StartMultiplyBar()
    {
        if (screen.MultiplyBar.value == screen.MultiplyBar.minValue)
        {
            screen.MultiplyBar.DOValue(screen.MultiplyBar.maxValue, multiplySpeed).SetEase(Ease.Linear).OnComplete(()=>StartMultiplyBar()).OnUpdate(()=>CheckMultiplyBar(screen.MultiplyBar.value));
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
        else if(value>0.150 && value <0.350)
        {
            UpdateMultiplyText(3);
        }
        else if(value>0.350 && value <0.620)
        {
            UpdateMultiplyText(5);
        }
        else if(value>0.620 && value < 0.830)
        {
            UpdateMultiplyText(3);
        }
        else if(value>0.830 && value <= 1)
        {
            UpdateMultiplyText(2);
        }
    }

    private void UpdateMultiplyText(int multiply)
    {
        screen.MultiplyText.text = (income * multiply).ToString();
    }

    private void GetPrize()
    {
        screen.MultiplyBar.DOKill();
    }

    private void NextLevel()
    {
        Bootstrap.Instance.SaveGame();
        Bootstrap.Instance.GameRestart(0);
    }
}