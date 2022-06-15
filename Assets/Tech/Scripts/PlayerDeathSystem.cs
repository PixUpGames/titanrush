using Kuhpik;
using System.Collections;
using UnityEngine;

public class PlayerDeathSystem : GameSystem
{
    [SerializeField] private float waitToKick = 1f;
    [SerializeField] private float waitToPlayerDie = .5f;
    public override void OnStateEnter()
    {
        StartCoroutine(PlayerDeathCourutine());
    }

    private IEnumerator PlayerDeathCourutine()
    {
        game.PlayerComponent.PlayerAnimator.SetFightIdle(true);
        game.enemyBoss.StartBattle();

        yield return new WaitForSeconds(waitToKick);

        game.enemyBoss.DoKick();

        yield return new WaitForSeconds(waitToPlayerDie);

        game.PlayerComponent.PlayerAnimator.Die();

        yield return new WaitForSeconds(waitToPlayerDie);

        Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
    }
}