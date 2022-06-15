using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI coinsCounterText;
    [SerializeField] private TextMeshProUGUI levelCounterText;

    [field: SerializeField] public Button settingsButton { get; set; }

    public void UpdateCoinsCounter(int value)
    {
        coinsCounterText.text = $"{value}";
    }
    public void UpdateLevelCounter(int level)
    {
        levelCounterText.text = $"Lvl. {level}";
    }
}