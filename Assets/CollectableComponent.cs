using UnityEngine;

public class CollectableComponent : MonoBehaviour
{
    [SerializeField] private Collectable collectableType;

    public Collectable GetCollectable => collectableType;
}

public enum Collectable
{
    COIN = 1,
    POWER_UP = 2,
    POWER_DOWN = 3
}