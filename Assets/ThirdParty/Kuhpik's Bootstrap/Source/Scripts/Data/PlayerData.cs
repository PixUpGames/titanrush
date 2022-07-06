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
        public int Level;
        public float Money;

        public List<CustomizableType> OpenedCustomizables = new List<CustomizableType>();

        public CustomizableType hatType=CustomizableType.Null;
        public CustomizableType glovesType=CustomizableType.Null;
        public CustomizableType skinType=CustomizableType.Null;

        public float DistanceUpgrade;
        public float SpeedUpgrade;

        public int DistanceUpgradeLevel;
        public int SpeedUpgradeLevel;

        public int GlovesIndex;
        public int HatsIndex;
        public int SkinsIndex;
    }
}