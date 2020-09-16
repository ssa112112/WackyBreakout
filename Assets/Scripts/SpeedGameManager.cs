using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Speed manager for use speedupEffect
/// </summary>
public class SpeedGameManager : MonoBehaviour
{
    UFloat timeToNormalSpeed;

    private void Start()
    {
        //Become listener
        EventsManager.AddSpeedupEffectListener(SpeedupEffectActivated);
    }

    private void Update()
    {
        //Check time to normal speed
        if (timeToNormalSpeed != 0 && !PauseWaiter.IsPauseMod())
        {
            timeToNormalSpeed -= (UFloat)Time.unscaledDeltaTime;
            //Stop speedupEffect, if it is the time
            if (timeToNormalSpeed == 0 && !EndGameWaiter.IsGameOver())
            {
                AudioManager.Play(AudioName.EffectSpeedupDeactivated);
                Time.timeScale = 1;
            }
        }
    }

    
    private void SpeedupEffectActivated(float duration,float force)
    {
        if (Time.timeScale == 1)
            Time.timeScale *= force;
        timeToNormalSpeed += (UFloat)duration;
    }
}
