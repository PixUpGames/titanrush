using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageComponent : MonoBehaviour
{
    [SerializeField] private ShopType shopType;
    [SerializeField] private Transform parentObj;
    [SerializeField] private Button getRandomItem;
    [SerializeField] private Button getRewardMoney;

    public Transform ParentObj => parentObj;
    public ShopType ShopType => shopType;
    public Button GetRandomItem => getRandomItem;
    public Button GetRewardMoney => getRewardMoney;
}
