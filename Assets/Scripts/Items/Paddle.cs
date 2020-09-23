using UnityEngine;

/// <summary>
/// A Paddle
/// </summary>
public class Paddle : MonoBehaviour
{
    Rigidbody2D rb2d;

    //Support with move
    float frozenPositionY;

    //Support with rebound a ball
    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;
    float colliderWidthHalf;
    float colliderHeightHalf;

    //Support with freezen effect
    UFloat timeToUnfrozen;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        frozenPositionY = rb2d.position.y;
        colliderWidthHalf = GetComponent<CapsuleCollider2D>().size.x / 2;
        colliderHeightHalf = GetComponent<CapsuleCollider2D>().size.y / 2;
        //Listen freezerEffect
        EventsManager.AddFreezerEffectListener(FreezerEffectActivated);
    }

    /// <summary>
    /// Move
    /// </summary>
    private void FixedUpdate()
    {
        //Check freezen
        if (timeToUnfrozen == 0)
        {
            //Check input
            var input = Input.GetAxis("Horizontal");
            if (input != 0)
            {
                //Calculate new position
                var targetPosition = new Vector2
                {
                    x = rb2d.position.x + (input * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedUnscaledDeltaTime),
                    y = frozenPositionY
                };
                //Apply new position
                rb2d.MovePosition(targetPosition);
            }
        }
        else
        {
            timeToUnfrozen -= (UFloat)Time.fixedUnscaledDeltaTime;
            if (timeToUnfrozen == 0)
                AudioManager.Play(AudioName.EffectFreezerDeactivated);

            //Activate the animation for unfreezing
            if (timeToUnfrozen < 0.5f) //0.5f it's the current duration of the animation
            {
                animator.SetBool("IsFreezing", false);
            }
        }
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="collision">collision info</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && CollisionOnTheTop(collision))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                collision.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                colliderWidthHalf;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            // tell the ball to set direction to new direction
            Ball ballScript = collision.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    /// <summary>
    /// Check a collision place, true if is it the top or near 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool CollisionOnTheTop(Collision2D collision)
    {
        //"colliderHeightHalf / 10" for check near collisions
        if (collision.contacts[0].point.y - colliderHeightHalf / 10 > transform.position.y)
            return true;
        return false;
    }

    private void FreezerEffectActivated(float freezerEffectDuration)
    {
        timeToUnfrozen += (UFloat)freezerEffectDuration;

        //Activate the animation for freezing
        animator.SetBool("IsFreezing",true);
    }
}