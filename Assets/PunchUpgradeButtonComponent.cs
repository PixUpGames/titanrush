using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PunchUpgradeButtonComponent : MonoBehaviour
{
    public TMP_Text PriceText;
    public TMP_Text UpgradeText;
    public Transform RewardHUD;
    public Transform BuyHUD;
    public Button UpgradeButton;

    public void UpdateInfo(int price,float upgradeLevel)
    {
        PriceText.text = price.ToString();
        UpgradeText.text = upgradeLevel.ToString("0.0");
    }

    public void SetRewardStatus()
    {
        RewardHUD.gameObject.SetActive(true);
        BuyHUD.gameObject.SetActive(false);
    }
}
