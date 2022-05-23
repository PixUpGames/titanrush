using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUIScreen : UIScreen
{
    [field: SerializeField] public Button RestartButton { get; set; }
    [field: SerializeField] public CanvasGroup ScreenCanvasGroup { get; set; }
}