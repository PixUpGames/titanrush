using UnityEngine;

public class CustomizablePickUpComponent : CollectableComponent
{
    [SerializeField] private CustomizableType itemType;

    public CustomizableType GetItemType => itemType;
}
public enum CustomizableType
{
    Null = 1,
    CLAWS = 2,
    BOX_GLOVES = 3,
    FISTS = 4,
    KNUCKLES = 5,
    MORGEN = 6,
    PAWS = 7,
    HAT_CAP = 8,
    HAT_HELMET = 9,
    HAT_SATAN = 10,
    HAT_SHAPKA = 11,
    HAT_VIKING = 12,
    HAT_WIZZARD = 13,
    SKIN_1=14,
    SKIN_2=15,
    SKIN_3=16,
    SKIN_4=17,
    SKIN_5=18,
    SKIN_6=19
}