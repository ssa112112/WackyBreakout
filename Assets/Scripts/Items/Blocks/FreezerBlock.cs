﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezerBlock : Block
{
    //Support with effect
    float freezerEffectDuration;
    FreezerEffectActivated freezerEffectActivatedEvent = new FreezerEffectActivated();

    //AnimationSpeed
    const float animationSpeed = 0.009f;

    protected override void Start()
    {
        //Set scorePoints
        scorePoints = ConfigurationUtils.FreezerBlockPoints;
        //Get setting
        freezerEffectDuration = ConfigurationUtils.FreezerEffectDuration;
        //Become invoker
        EventsManager.AddFreezerEffectInvoker(freezerEffectActivatedEvent);
        base.Start();
    }

    protected override void SetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
        StartCoroutine(ColorAnimation());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            freezerEffectActivatedEvent.Invoke(freezerEffectDuration);
            AudioManager.Play(AudioName.EffectFreezerActivated);
        }
        base.OnCollisionEnter2D(collision);
    }

    public void AddFreezerEffectListener(UnityAction<float> action)
    {
        freezerEffectActivatedEvent.AddListener(action);
    }
}
