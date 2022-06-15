using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class MultiplierEnterSystem : GameSystem
{
    [SerializeField, Tag] private string multiplierTag;

    [SerializeField] private Material activatedMaterial;

    public override void OnStateEnter()
    {
        game.enemyBoss.OnTriggerEnter.OnEnter += SetMultiplier;
    }
    public override void OnStateExit()
    {
        game.enemyBoss.OnTriggerEnter.OnEnter -= SetMultiplier;        
    }
    private void SetMultiplier(Transform other, Transform @object)
    {
        if (other.CompareTag(multiplierTag))
        {
            var multiplierComp = other.GetComponent<MultiplierComponent>();
            game.Multiplier = multiplierComp.Multiplier;
            multiplierComp.EnableMeshRenderer();
        }
    }
}