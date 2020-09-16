using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Customize message about game over 
/// </summary>
public class GameOverText : MonoBehaviour
{
    void Start()
    {
       gameObject.GetComponent<Text>().text =
            (EndGameWaiter.IsWon() ? "YOU WON" : "YOU LOSE") + Environment.NewLine +$"score: {HUG.GetCurrentScore()}";
    }
}
