using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemConfig", fileName = "ItemConfig")]
public class ShopItemConfig : ScriptableObject
{
    [SerializeField] private CustomizableType customizableType;
    [SerializeField] private ShopType shopType;
    [SerializeField] private Sprite icon;

    public CustomizableType CustomizableType => customizableType;
    public ShopType ShopType => shopType;
    public Sprite Icon => icon;
}
