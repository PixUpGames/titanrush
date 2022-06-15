using Sirenix.OdinInspector;
using UnityEngine;

public class MultiplierCalculator : MonoBehaviour
{
    [SerializeField] private MultiplierComponent[] multipliers;
    [SerializeField] private float startMultiplier = 6f;
    [SerializeField] private float multiplierStep = 0.1f;

    [Button]
    public void CalculateMultipliers()
    {
        for (int i = 0; i < multipliers.Length; i++)
        {
            multipliers[i].SetMultiplier(startMultiplier + multiplierStep * i);
        }
    }
}