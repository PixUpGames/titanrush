using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class EnemyBreakWallsSystem : GameSystem
{
    [SerializeField, Tag] private string wallsTag;

    private int explodeHash;

    private const string WALLS_EXPLODE = "Explode";

    public override void OnInit()
    {
        explodeHash = Animator.StringToHash(WALLS_EXPLODE);

        game.enemyBoss.OnTriggerEnter.OnEnter += ExplodeWall;
    }

    private void ExplodeWall(Transform other, Transform @object)
    {
        if (other.CompareTag(wallsTag))
        {
            other.GetComponent<BreakableWallComponent>().Animate(explodeHash);
        }
    }
}