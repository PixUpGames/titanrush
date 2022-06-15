using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUIScreen : UIScreen
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI coinsText;

    public Button ContinueButton => continueButton;

    public void InitScreen(float multiplierValue, float coinsValue)
    {
        multiplierText.text = $"{multiplierValue}X";
        coinsText.text = $"{(int) coinsValue}<sprite=0>";
    }
}