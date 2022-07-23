using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // public float LevelProgress;
        // public Enemy[] Enemies;

        public PlayerComponent PlayerComponent;
        public int Coins;
        public float MutationBars;
        public float Multiplier;
        public Level LevelConfig;
        public CamerasComponent Cameras;
        public FinishComponent Finish;
        public EnemyComponent enemyBoss;
        public float playerSpeed;
        public EnemyState punchAndDodgeState;
        public float IncomeMultiply;
        public int MutationLevel;

        public bool isHammerFinished;

        public IEnumerator blinkHPRoutine;

        public IEnumerator BlinkHPBar(Image targetImage,float time,Color mainColor)
        {
            targetImage.color = mainColor;
            for (int i = 0; i < 6; i++)
            {
                targetImage.color = Color.white;
                yield return new WaitForSeconds(time);
                targetImage.color = mainColor;
            }
            targetImage.color = mainColor;
        }
    }
}