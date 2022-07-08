using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class FightingScreenUI : UIScreen
{
    [SerializeField] private Image powerSlider;

    [SerializeField] private Image playerRealSlider;
    [SerializeField] private Image playerFakeSlider;
    [SerializeField] private Image enemyRealSlider;
    [SerializeField] private Image enemyFakeSlider;

    public GameObject PowerBar;

    public void UpdatePowerSlider(float value, float maxValue)
    {
        powerSlider.fillAmount = value / maxValue;
    }

    public void UpdatePlayerSliders(float value, float fakeValue, float maxValue)
    {
        playerRealSlider.fillAmount = value / maxValue;
        playerFakeSlider.fillAmount = fakeValue / maxValue;
    }
    public void UpdateEnemySliders(float value, float fakeValue, float maxValue)
    {
        enemyRealSlider.fillAmount = value / maxValue;
        enemyFakeSlider.fillAmount = fakeValue / maxValue;
    }
}