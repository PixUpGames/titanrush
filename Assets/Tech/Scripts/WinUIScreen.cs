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
    [SerializeField] private Image shadowImage;
    [SerializeField] private Image skinImage;
    [SerializeField] private Button rewardSkinButton;
    [SerializeField] private ParticleSystem skinParticle;

    public Button PrizeButton => prizeButton;
    public Button ContinueButton => continueButton;
    public Slider MultiplyBar => multiplyPriceBar;
    public TMP_Text MultiplyText => multiplierText;
    public TMP_Text CoinsText => coinsText;
    public TMP_Text LevelMultiply => levelMultiply;
    public TMP_Text WinCoinCount => winCoinCount;
    public Image ShadowImage => shadowImage;
    public Image SkinImage => skinImage;
    public Button RewardSkinButton => rewardSkinButton;
    public ParticleSystem SkinParticle => skinParticle;

    public void InitScreen(float multiplierValue, float coinsValue)
    {
        levelMultiply.text = $"{multiplierValue}X";
        winCoinCount.text = $"{(int) coinsValue}";
    }
}