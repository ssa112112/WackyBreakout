using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block
{
    //AnimationSpeed
    const float animationSpeed = 0.004f;

    protected override void Start()
    {
        //Set scorePoints
        scorePoints = ConfigurationUtils.BonusBlockPoints;
        base.Start();
    }

    protected override void SetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        StartCoroutine(ColorAnimation());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            AudioManager.Play(AudioName.BonusReceived);
        }
        base.OnCollisionEnter2D(collision);
    }
}
