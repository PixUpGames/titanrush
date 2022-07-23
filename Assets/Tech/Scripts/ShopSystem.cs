using DG.Tweening;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ShopType
{
    GLOVES=0,
    HAT=1,
    SKINS=2
}

public class ShopSystem : GameSystemWithScreen<ShopUIScreen>
{
    [SerializeField] private ShopItemComponent shopItemPrefab;
    [SerializeField] private string configPath;
    [SerializeField] private ShopItemConfig[] itemConfigs;
    [SerializeField] Dictionary<CustomizableType, ShopItemComponent> shopButtons = new Dictionary<CustomizableType, ShopItemComponent>();
    private Dictionary<ShopType, ShopPageComponent> shopPages = new Dictionary<ShopType, ShopPageComponent>();

    public override void OnInit()
    {
        InitShop();

        screen.OpenGlovesButton.onClick.AddListener(() => OpenShopPage(ShopType.GLOVES));
        screen.OpenHatsButton.onClick.AddListener(() => OpenShopPage(ShopType.HAT));
        screen.OpenSkinsButton.onClick.AddListener(() => OpenShopPage(ShopType.SKINS));
        screen.CloseShop.onClick.AddListener(() => CloseShop());
    }

    private void InitShop()
    {
        LoadShopConfigs();
        InitShopButtons();
        LoadSavedShopData();
    }

    private void CloseShop()
    {
        screen.ShopWindow.transform.DOScale(Vector3.one * 0.05f, 0.15f).OnComplete(() => screen.ShopWindow.gameObject.SetActive(false));
    }

    private void LoadShopConfigs()
    {
        shopPages = screen.GetComponentsInChildren<ShopPageComponent>(true).ToDictionary(x => x.ShopType, x => x);
        itemConfigs = Resources.LoadAll<ShopItemConfig>(configPath);

        foreach (var page in shopPages)
        {
            page.Value.GetRandomItem.onClick.AddListener(() => GetRandomItem(page.Value.ShopType));
            page.Value.GetRewardMoney.onClick.AddListener(() => GetRewardMoney(page.Value.ShopType));

            UpdatePageIncomesButtons(page.Key, page.Value);
        }
    }

    private void UpdatePageIncomesButtons(ShopType type, ShopPageComponent page)
    {
        if (type == ShopType.GLOVES)
        {
            screen.GlovesRandomPrice.text = ((player.GlovesIndex + 1) * 1000).ToString();
            screen.GlovesRewardIncome.text = ((player.GlovesIndex + 1) * 1000).ToString();

            if (GetRandomItemPrice(type) > player.Money)
                page.GetRandomItem.interactable = false;
        }
        else if (type == ShopType.HAT)
        {
            screen.HatsRandomIncome.text = ((player.HatsIndex + 1) * 1000).ToString();
            screen.HatsRewardIncome.text = ((player.HatsIndex + 1) * 1000).ToString();

            if (GetRandomItemPrice(type) > player.Money)
                page.GetRandomItem.interactable = false;
        }
        else
        {
            screen.SkinRandomIncome.text = ((player.SkinsIndex + 1) * 1000).ToString();
            screen.SkinRewardIncome.text = ((player.SkinsIndex + 1) * 1000).ToString();

            if (GetRandomItemPrice(type) > player.Money)
                page.GetRandomItem.interactable = false;
        }
    }

    private void InitShopButtons()
    {
        foreach (var config in itemConfigs)
        {
            var newButton = Instantiate(shopItemPrefab, shopPages[config.ShopType].ParentObj);
            newButton.InitItem(config.Icon, false, config.CustomizableType, config.ShopType);
            newButton.ItemButton.onClick.AddListener(() => newButton.GetButtonAction());
            shopButtons.Add(config.CustomizableType, newButton);
        }
    }

    private void LoadSavedShopData()
    {
        if (player.OpenedCustomizables.Count > 0)
        {
            foreach (var button in shopButtons)
            {
                foreach (var item in player.OpenedCustomizables)
                {
                    if (item == button.Value.Type)
                    {
                        button.Value.SetStatus(true);
                    }
                }
            }
        }

        if (player.hatType != CustomizableType.Null)
        {
            shopButtons[player.hatType].WearItem();
        }

        if (player.glovesType != CustomizableType.Null)
        {
            shopButtons[player.glovesType].WearItem();
        }

        if (player.skinType != CustomizableType.Null)
        {
            shopButtons[player.skinType].WearItem();
        }
    }

    private void OpenShopPage(ShopType type)
    {
        foreach (var page in shopPages)
        {
            if (page.Key == type)
            {
                page.Value.gameObject.SetActive(true);
            }
            else
            {
                page.Value.gameObject.SetActive(false);
            }
        }
    }

    private void GetRandomItem(ShopType type)
    {
        List<ShopItemComponent> tempTargetItems = new List<ShopItemComponent>();

        player.Money -= GetRandomItemPrice(type);
        UIManager.GetUIScreen<GameUIScreen>().UpdateCoinsCounter(player.Money);

        foreach (var shopItem in shopButtons)
        {
            if (shopItem.Value.ShopType == type && shopItem.Value.IsOpen == false)
            {
                tempTargetItems.Add(shopItem.Value);
            }
        }

        int randomItem = Random.Range(0, tempTargetItems.Count - 1);

        tempTargetItems[randomItem].GetButtonAction();
        shopPages[type].GetRandomItem.interactable = false;

        IncreaseItemIndex(type);
        Debug.Log("[REWARD] Get Random Item by type ==> " + type);
    }

    private int GetRandomItemPrice(ShopType type)
    {
        if (type == ShopType.GLOVES)
        {
            return (player.GlovesIndex + 1) * 1000;
        }
        else if (type == ShopType.HAT)
        {
            return (player.HatsIndex + 1) * 1000;
        }
        else
        {
            return (player.SkinsIndex + 1) * 1000;
        }
    }

    private void IncreaseItemIndex(ShopType type)
    {
        if (type == ShopType.GLOVES)
        {
            player.GlovesIndex++;
        }
        else if (type == ShopType.HAT)
        {
            player.HatsIndex++;
        }
        else
        {
            player.SkinsIndex++;
        }
    }

    private void GetRewardMoney(ShopType type)
    {
        player.Money += GetRandomItemPrice(type);
        shopPages[type].GetRewardMoney.interactable = false;
        UIManager.GetUIScreen<GameUIScreen>().UpdateCoinsCounter(player.Money);
        Debug.Log("[REWARD] Get Reward Money");
    }

    public void AddOpenedItem(CustomizableType itemType)
    {
        player.OpenedCustomizables.Add(itemType);
    }

    public void SetCurrentItem(ShopType shopType, CustomizableType customizableType)
    {
        if (shopType == ShopType.GLOVES)
        {
            player.glovesType = customizableType;
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(shopType, customizableType);

        }
        else if (shopType == ShopType.HAT)
        {
            player.hatType = customizableType;
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(shopType, customizableType);
        }
        else
        {
            player.skinType = customizableType;
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(shopType, customizableType);
        }

        foreach (var button in shopButtons)
        {
            if (button.Value.ShopType == shopType)
            {
                if (button.Value.Type != customizableType)
                {
                    button.Value.DeSetItem();
                }
            }
        }
    }
}
