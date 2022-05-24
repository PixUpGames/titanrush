using Kuhpik;
using UnityEngine;

public class FightPunchingSystem : GameSystem
{
    [SerializeField] private float powerIncreaseOnClick = 1f;
    [SerializeField] private float maxPowerValue = 5f;

    private float powerValue = 0;

    public override void OnStateEnter()
    {
        
    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            powerValue = Mathf.Clamp(powerValue + powerIncreaseOnClick, 0, maxPowerValue);
        }
        else
        {
            powerValue = Mathf.Clamp(powerValue - Time.deltaTime, 0, maxPowerValue);
        }
    }

}