using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class FightingScreenUI : UIScreen
{
    [SerializeField] private Image powerSlider;

    public void UpdateSlider(float value, float maxValue)
    {
        powerSlider.fillAmount = value / maxValue;
    }
}