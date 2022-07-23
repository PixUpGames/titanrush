using DG.Tweening;
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
    [SerializeField] private GameObject tapAnim;
    [SerializeField] private GameObject dapAnim;

    private bool isPunched;

    public Color playerColor;
    public Color enemyColor;
    public GameObject hpBarHolder;
    public GameObject PowerBar;
    public Image PunchRedOffset;
    public Transform PlayerHPBar;
    public Transform EnemyHPBar;

    public GameObject TapAnim => tapAnim;
    public GameObject DapAnim => dapAnim;
    public Image EnemyFakeSlider => enemyFakeSlider;
    public Image PlayerFakeSlider => playerFakeSlider;

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

    public void PunchHPBar(Transform targetBar)
    {
        if (!isPunched)
        {
            isPunched = true;
            targetBar.DOPunchScale(Vector3.one * 0.05f, 0.3f).OnComplete(() => isPunched = false);
        }
    }
}