using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemComponent : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button itemButton;
    [SerializeField] private GameObject activeHUD;
    [SerializeField] private GameObject inactiveHUD;
    [SerializeField] private CustomizableType type;
    [SerializeField] private ShopType shopType;
    [SerializeField] private GameObject openEffect;
    private bool isOpen;
    private bool isChoosen;
    public Button ItemButton => itemButton;
    public CustomizableType Type => type;
    public ShopType ShopType => shopType;

    public void InitItem(Sprite configSprite,bool isOpen,CustomizableType type,ShopType shopType)
    {
        icon.sprite = configSprite;
        this.isOpen = isOpen;
        this.type = type;
        this.shopType = shopType;
    }

    public void GetButtonAction()
    {
        if (isOpen == false)
        {
            Bootstrap.Instance.GetSystem<ShopSystem>().AddOpenedItem(type);
            isOpen = true;
            //Debug.Log("Reward");
            WearItem();
        }
        else
        {
            WearItem();
        }
    }

    public void SetStatus(bool status)
    {
        isOpen = status;
        if (isOpen == false)
        {
            inactiveHUD.SetActive(true);
            activeHUD.SetActive(false);
        }
        else
        {
            inactiveHUD.SetActive(false);
            activeHUD.SetActive(true);
        }
    }

    public void WearItem()
    {
        if (isOpen == true)
        {
            if (isChoosen == false)
            {
                isChoosen = true;
                inactiveHUD.SetActive(false);
                activeHUD.SetActive(true);
                openEffect.SetActive(true);
                Bootstrap.Instance.GetSystem<ShopSystem>().SetCurrentItem(shopType, type);
            }
            else
            {
                DeSetItem();
                Bootstrap.Instance.GetSystem<ShopSystem>().SetCurrentItem(shopType, CustomizableType.Null);
            }
        }
    }

    public void DeSetItem()
    {
        isChoosen = false;
        openEffect.SetActive(false);
    }
}
