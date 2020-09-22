using UnityEngine;

public class BonusBlock : Block
{
    //AnimationSpeed 
    const float animationSpeed = 0.015f;

    protected override void Start()
    {
        //Set scorePoints
        scorePoints = ConfigurationUtils.BonusBlockPoints;
        base.Start();
    }

    protected override void SetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        StartCoroutine(ColorAnimation(animationSpeed));
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioName.BonusReceived);
        }
        base.OnCollisionEnter2D(collision);
    }
}
