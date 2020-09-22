using System.Collections;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected int scorePoints = 5;

    AddPoints addPointsEvent = new AddPoints();
    BlockDestroyed blockDestroyed = new BlockDestroyed();

    protected virtual void Start()
    {
        SetColor();
        //Become invoker to add points
        EventsManager.AddPointsInvoker(addPointsEvent);
        //Become invoker to block destroyed
        EventsManager.AddBlockDestroyedInvoker(blockDestroyed);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            blockDestroyed.Invoke();
            addPointsEvent.Invoke(scorePoints);
            Destroy(this.gameObject);
        }
    }
    protected abstract void SetColor();

    /// <summary>
    /// Change color.g component up and down
    /// </summary>
    protected IEnumerator ColorAnimation(float animationSpeed)
    {
        //Set const
        const float animationProcent = 0.2f;
        //Get needly things
        var spriteRednder = GetComponent<SpriteRenderer>();
        Color currentColor;
        currentColor = spriteRednder.color;
        float maxG = Mathf.Min(1f, Mathf.Max(animationProcent, currentColor.g * (1f + animationProcent)));
        float minG = currentColor.g * (1 - animationProcent);
        //Set some random color
        currentColor.g = Random.Range(minG, maxG);
        while (true)
        {
            while (currentColor.g > minG)
            {
                //Color--
                currentColor.g -= animationSpeed;
                spriteRednder.color = currentColor;
                yield return null;
            }
            while (currentColor.g < maxG)
            {
                //Color++
                currentColor.g += animationSpeed;
                spriteRednder.color = currentColor;
                yield return null;
            }
        }
    }

    /// <summary>
    /// Change color.g component up and down with standart speed
    /// </summary>
    protected IEnumerator ColorAnimation()
    {
        return ColorAnimation(animationSpeed: 0.015f);
    }
}
