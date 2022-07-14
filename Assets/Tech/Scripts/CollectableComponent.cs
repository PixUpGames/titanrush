using DG.Tweening;
using UnityEngine;

public class CollectableComponent : MonoBehaviour
{
    [SerializeField] private Collectable collectableType;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool isCannon;
    private bool isMoving;
    public Collectable GetCollectable => collectableType;

    public Collider Collider;

    private void Awake()
    {
        if (isCannon == false)
            transform.DORotate(rotation, 1f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        else
            isMoving = true;
    }

    private void Update()
    {
        if (isMoving == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        }
    }
}

public enum Collectable
{
    COIN = 1,
    POWER_UP = 2,
    POWER_DOWN = 3,
    CUSTOMIZABLE = 4
}