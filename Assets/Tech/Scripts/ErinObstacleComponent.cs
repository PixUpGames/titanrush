using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErinObstacleComponent : MonoBehaviour
{
    public Collider obstacleCollider;
    public Transform aimTarget;
    public Transform startPos;
    public Transform endPos;
    public Animator obstacleAnimator;

    private const string attack = "Level_Attack";
    private int attackHash;

    public void Start()
    {
        obstacleAnimator = GetComponent<Animator>();
        obstacleAnimator.Play("Level_Idle");
        attackHash = Animator.StringToHash(attack);
    }

    public void StartAttack()
    {
        aimTarget.transform.position = startPos.position;
        obstacleCollider.enabled = false;
        aimTarget.DOMove(endPos.transform.position, 0.7f).OnComplete(()=>Attack()).SetEase(Ease.Linear);
    }

    public void Attack()
    {
        obstacleAnimator.SetTrigger(attackHash);
    }

    public void ToggleCollider(int i)
    {
        if (i == 1)
        {
            obstacleCollider.enabled = true;
        }
        else
        {
            obstacleCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerComponent player))
        {
            StartAttack();
        }
    }
}
