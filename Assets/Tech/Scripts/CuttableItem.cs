using UnityEngine;

public abstract class CuttableItem : MonoBehaviour
{
    public abstract void Cut();
    public abstract int ReceiveAward();
}