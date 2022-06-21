using DG.Tweening;
using UnityEngine;

public class CollectableComponent : MonoBehaviour
{
    [SerializeField] private Collectable collectableType;
    [SerializeField] private Vector3 rotation;

    public Collectable GetCollectable => collectableType;

    private void Awake()
    {
        transform.DORotate(rotation, 1f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}

public enum Collectable
{
    COIN = 1,
    POWER_UP = 2,
    POWER_DOWN = 3,
    CUSTOMIZABLE = 4
}