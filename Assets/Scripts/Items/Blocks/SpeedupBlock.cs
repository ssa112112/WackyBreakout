using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedupBlock : Block
{
    //Support with effect
    SpeedupEffectActivated speedupEffectActivatedEvent = new SpeedupEffectActivated();
    float speedupEffectDuration;
    float speedupEffectForce;

    //AnimationSpeed
    const float animationSpeed = 0.008f;

    protected override void Start()
    {
        //Set scorePoints
        scorePoints = ConfigurationUtils.SpeedupBlockPoints;
        //Set effect settings
        speedupEffectDuration = ConfigurationUtils.SpedupEffectDuration;
        speedupEffectForce = ConfigurationUtils.SpeedupEffectForce;
        //Become invoker
        EventsManager.AddSpeedupEffectInvoker(speedupEffectActivatedEvent);
        base.Start();
    }

    protected override void SetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ColorAnimation());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //Run effect
        if (collision.gameObject.tag == "Ball")
        {
            speedupEffectActivatedEvent.Invoke(speedupEffectDuration, speedupEffectForce);
            AudioManager.Play(AudioName.EffectSpeedupActivated);
        }
        base.OnCollisionEnter2D(collision);
    }



    public void AddSpeedupEffectListener(UnityAction<float, float> action)
    {
        speedupEffectActivatedEvent.AddListener(action);
    }
}
