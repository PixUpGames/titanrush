using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // [BoxGroup("level")] public int level;
        // [BoxGroup("currency")] public int money;

        public int Level;
        public float Money;

        public List<CustomizableType> OpenedCustomizables = new List<CustomizableType>();

        public CustomizableType hatType = CustomizableType.Null;
        public CustomizableType glovesType = CustomizableType.Null;
        public CustomizableType skinType = CustomizableType.Null;

        public float DistanceUpgrade;
        public float SpeedUpgrade;

        public int DistanceUpgradeLevel;
        public int SpeedUpgradeLevel;

        public int GlovesIndex;
        public int HatsIndex;
        public int SkinsIndex;

        public bool isRevive;
        public Vector3 RevivePos;
    }
}