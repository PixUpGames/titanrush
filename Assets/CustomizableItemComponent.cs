using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableItemComponent : MonoBehaviour
{
    [SerializeField] private CustomizableType itemName;
    [SerializeField] private ShopType itemType;

    public CustomizableType ItemName => itemName;
    public ShopType ItemType => itemType;
}
