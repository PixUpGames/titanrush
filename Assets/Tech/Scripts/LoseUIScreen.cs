using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUIScreen : UIScreen
{
    [field: SerializeField] public Button RestartButton { get; set; }
    [field: SerializeField] public Button ReviveButton { get; set; }
    [field: SerializeField] public CanvasGroup ScreenCanvasGroup { get; set; }

    public Image fillTimer;
    public Text timer;
}