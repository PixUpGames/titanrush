using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIScreen : UIScreen
{
    [Header("Gloves UI Components")]
    public Button OpenGlovesButton;
    public Text GlovesRewardIncome;
    public TMP_Text GlovesRandomPrice;
    [Header("Hats UI Components")]
    public Button OpenHatsButton;
    public Text HatsRewardIncome;
    public TMP_Text HatsRandomIncome;
    [Header("Skins UI Components")]
    public Button OpenSkinsButton;
    public Text SkinRewardIncome;
    public TMP_Text SkinRandomIncome;
    [Header("ShopScreen")]
    public GameObject ShopWindow;
    [Header("CloseShop")]
    public Button CloseShop;
}
