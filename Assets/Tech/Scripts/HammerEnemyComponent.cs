using DG.Tweening;
using UnityEngine;

public class HammerEnemyComponent : EnemyComponent
{
    [SerializeField] private GameObject firstModel;
    [SerializeField] private GameObject secondModel;
    [SerializeField] private ParticleSystem mutateParticle;
    [SerializeField] private float scaleFactor;
    public override void Prepare()
    {
        secondModel.SetActive(true);
        firstModel.SetActive(false);
        secondModel.transform.DOScale(Vector3.one*scaleFactor,0.35f).OnComplete(()=>mutateParticle.Play());

        base.Prepare();
    }
}