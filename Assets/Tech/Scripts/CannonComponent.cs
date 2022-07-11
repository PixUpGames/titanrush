using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonComponent : CuttableItem
{
    [SerializeField] private Animator cannonAnimator;
    [SerializeField] private CollectableComponent spawnedProjectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private List<Transform> projectiles;
    [SerializeField] private ParticleSystem breakEffect;

    public CuttableType Type;
    [SerializeField] private int award;
    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            CollectableComponent projectile = Instantiate(spawnedProjectile, shootPoint.transform.position, shootPoint.transform.rotation);
            cannonAnimator.SetBool("Shoot", true);
            shootEffect.Play();
            projectiles.Add(projectile.transform);
            yield return new WaitForSeconds(5f);
        }
    }

    public override CuttableType GetCuttableType()
    {
        return Type;
    }

    public override void Cut()
    {
        var effect = breakEffect;
        breakEffect.transform.SetParent(null);
        effect.Play();
        gameObject.SetActive(false);
    }

    public override int ReceiveAward()
    {
        return award;
    }
}
