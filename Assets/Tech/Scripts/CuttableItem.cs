using UnityEngine;

public enum CuttableType
{
    Hand = 1,
    Head = 2,
    Silly_Titan = 3,
    Cannon
}

public abstract class CuttableItem : MonoBehaviour
{
    public abstract CuttableType GetCuttableType();
    public abstract void Cut();
    public abstract int ReceiveAward();
}