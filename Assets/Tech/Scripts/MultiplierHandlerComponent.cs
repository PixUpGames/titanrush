using UnityEngine;

public class MultiplierHandlerComponent : MonoBehaviour
{
    [SerializeField] private MultiplierComponent[] multipliers;

    public MultiplierComponent GetLastMultiplier(float multiplierValue)
    {
        for (int i = 0; i < multipliers.Length; i++)
        {
            if (multipliers[i].Multiplier == multiplierValue)
            {
                return multipliers[i];
            }
        }

        return multipliers[multipliers.Length - 1];
    }
}
