using DG.Tweening;
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

    private bool isPunched;

    public GameObject TempItemScreen;
    public Transform CoinBar;
    public Button GetItemButton;
    public Button LoseItemButton;
    public Image TempItemIcon;

    public void UpdateCoinsCounter(float value)
    {
        coinsCounterText.text = value.ToString("0");
    }
    public void UpdateLevelCounter(int level)
    {
        levelCounterText.text = $"Lvl. {(level+1)}";
    }

    public void PunchBar(Transform targetBar)
    {
        if (!isPunched)
        {
            isPunched = true;
            targetBar.DOPunchScale(Vector3.one * 0.1f, 0.3f).OnComplete(() => isPunched = false);
        }
    }
}