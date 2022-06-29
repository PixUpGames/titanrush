using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAPHealthSystem : GameSystemWithScreen<FightingScreenUI>
{
    [SerializeField] private float playerMaxHealth = 10;
    [SerializeField] private float enemyMaxHealth = 10;
    [SerializeField] private float lerpSpeed = 10;

    private float playerLerpedHealth;
    private float enemyLerpedHealth;

    public override void OnInit()
    {
        game.PlayerComponent.SetHealth(playerMaxHealth);
        game.enemyBoss.SetHealth(enemyMaxHealth);

        playerLerpedHealth = game.PlayerComponent.GetHealth();
        enemyLerpedHealth = game.enemyBoss.GetHealth();
    }
    public override void OnUpdate()
    {
        playerLerpedHealth = Mathf.Lerp(playerLerpedHealth, game.PlayerComponent.GetHealth(), Time.deltaTime * lerpSpeed);
        enemyLerpedHealth = Mathf.Lerp(enemyLerpedHealth, game.enemyBoss.GetHealth(), Time.deltaTime * lerpSpeed);

        UpdateHealthBars();
    }

    private void UpdateHealthBars()
    {
        var enemyHealth = game.enemyBoss.GetHealth();
        var playerHealth = game.PlayerComponent.GetHealth();

        screen.UpdateEnemySliders(enemyHealth, enemyLerpedHealth, enemyMaxHealth);
        screen.UpdatePlayerSliders(playerHealth, playerLerpedHealth, playerMaxHealth);
    }
}
