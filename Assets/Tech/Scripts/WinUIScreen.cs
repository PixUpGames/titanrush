using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUIScreen : UIScreen
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Slider multiplyPriceBar;
    [SerializeField] private Button prizeButton;
    [SerializeField] private TMP_Text winCoinCount;
    [SerializeField] private TMP_Text levelMultiply;

    public Button PrizeButton => prizeButton;
    public Button ContinueButton => continueButton;
    public Slider MultiplyBar => multiplyPriceBar;
    public TMP_Text MultiplyText => multiplierText;
    public TMP_Text CoinsText => coinsText;
    public TMP_Text LevelMultiply => levelMultiply;
    public TMP_Text WinCoinCount => winCoinCount;

    public void InitScreen(float multiplierValue, float coinsValue)
    {
        levelMultiply.text = $"{multiplierValue}X";
        winCoinCount.text = $"{(int) coinsValue}<sprite=0>";
    }
}