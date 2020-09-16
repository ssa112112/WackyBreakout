using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    [SerializeField]
    Behaviour Halo;
    Rigidbody2D rb2d;

    //Support with correcting stucks (застреваниями в текстурах)
    float powerRebound = 10f;

    //Support with blinking
    float procentLifeBeforeBlinking = 0.75f; //[0f...1f]
    float blinkRate = 0.15f;

    //Support with spawn a replacement
    DisappearingBall disappearingBall = new DisappearingBall();

    //Support with remaining balls left
    ReduceBallsLeft reduceBallsLeft = new ReduceBallsLeft();

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //Really Start in TrueStart
        Invoke(nameof(TrueStart), ConfigurationUtils.TimeBeforeBallStartMoving);
        //Become invoker
        EventsManager.AddReduceBallsLeftInvoker(reduceBallsLeft);
        EventsManager.AddDisappearingBallInvoker(disappearingBall);
    }

    void TrueStart()
    {
        //Give starting force
        rb2d.AddForce(Vector2.down * ConfigurationUtils.BallImpulseForce);
        //And destroy after ballLifeTime secconds
        Destroy(gameObject, ConfigurationUtils.BallLifeTime);
        //Activate blinking, but not right now
        StartCoroutine(Blinking(ConfigurationUtils.BallLifeTime * procentLifeBeforeBlinking));
    }

    /// <summary>
    /// Change direction without changing speed
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector2 direction)
    {
        rb2d.velocity = rb2d.velocity.magnitude * direction;
    }

    /// <summary>
    /// Detect stucks and add force in the opposite(from poin's stuck) direction
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        //It's a border?
        if (collision.gameObject.tag == "MainCamera")
        {
            //Add horizontal force
            if (collision.contacts[0].point.x > 0)
                rb2d.AddForce(Vector2.left * powerRebound);
            else rb2d.AddForce(Vector2.right * powerRebound);

            //Add vertical force
            if (collision.contacts[0].point.y > 0)
                rb2d.AddForce(Vector2.down * powerRebound);
        }
    }

    /// <summary>
    /// Blinking the halo
    /// </summary>
    /// <param name="waitingBeforeStart"></param>
    /// <returns></returns>
    IEnumerator Blinking(float waitingBeforeStart)
    {
        //Wait its time
        if (waitingBeforeStart != 0)
            yield return new WaitForSeconds(waitingBeforeStart);

        //Start blinking
        bool changing = false;
        while (true)
        {
            changing = !changing;
            if (changing)
                Halo.GetType().GetProperty("enabled").SetValue(Halo, true, null);
            else
                Halo.GetType().GetProperty("enabled").SetValue(Halo, false, null);
            yield return new WaitForSeconds(blinkRate);
        }
    }

    /// <summary>
    /// Play collision sound
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Play(AudioName.BallCollision);   
    }

    /// <summary>
    /// Destroy the ball if it isn't on the screen and add new ball
    /// </summary>
    private void OnBecameInvisible()
    {
        if (transform.position.y < ScreenUtils.ScreenBottom)
        {
            AudioManager.Play(AudioName.BallLost);
            reduceBallsLeft.Invoke();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Send message about its dead
    /// </summary>
    private void OnDestroy()
    {
        CancelInvoke();
        disappearingBall.Invoke();
    }
}