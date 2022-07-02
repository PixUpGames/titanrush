using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI coinsCounterText;
    [SerializeField] private TextMeshProUGUI levelCounterText;
    public TMP_Text multiplyText;
    public Slider DapBar;
    [field: SerializeField] public Button settingsButton { get; set; }

    public void UpdateCoinsCounter(float value)
    {
        coinsCounterText.text = $"{value}";
    }
    public void UpdateLevelCounter(int level)
    {
        levelCounterText.text = $"Lvl. {(level+1)}";
    }
}