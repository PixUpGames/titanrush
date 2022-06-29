using UnityEngine;

public class HammerEnemyComponent : EnemyComponent
{
    [SerializeField] private GameObject firstModel;
    [SerializeField] private GameObject secondModel;
    public override void Prepare()
    {
        secondModel.SetActive(true);
        firstModel.SetActive(false);

        base.Prepare();
    }
}