using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab;

    //Support with check spawn location
    bool retrySpawn = false;
    Vector2 spawnLocationMin;
    Vector2 spawnLocationMax;

    //Support with respawn
    bool respawnAfterAnyDisappearingBall = false;
    bool respawnAfterBallLeavesScene = true; //used only if respawnAfterAnyDisappearingBall == false;

    private void Start()
    {
        SaveSpawnLocation();
        StartCoroutine(AutoSpawner());
        //BecomeListener
        if (respawnAfterAnyDisappearingBall)
            EventsManager.AddDisappearingBallListener(SpawnBallWithSomeWaiting);
        else if (respawnAfterBallLeavesScene)
            EventsManager.AddReduceBallsLeftListener(SpawnBallWithSomeWaiting);
    }

    private void Update()
    {
        if (retrySpawn)
        {
            SpawnBall();
        }
    }

    /// <summary>
    /// AutoSpawn balls
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoSpawner()
    {
        while (true)
        {
            SpawnBall();
            yield return new WaitForSeconds(Random.Range(ConfigurationUtils.SpawnTimeMin, ConfigurationUtils.SpawnTimeMax));
        } 
    }

    /// <summary>
    /// Spawn the ball right now, if it's possible
    /// </summary>
    public void SpawnBall()
    {
        if (IsSpawnLocationFree())
        {
            AudioManager.Play(AudioName.BallSpawn);
            Instantiate(ballPrefab);
            retrySpawn = false;
        }
        else
            retrySpawn = true;
    }

    /// <summary>
    /// Spawn the ball after some waiting
    /// </summary>
    void SpawnBallWithSomeWaiting()
    {
        float seccondsWait = Random.Range(ConfigurationUtils.RespawnTimeMin, ConfigurationUtils.RespawnTimeMax);
        Invoke(nameof(SpawnBall), seccondsWait);
    }

    /// <summary>
    /// Save spawn location
    /// </summary>
    private void SaveSpawnLocation()
    {
        var tempBall = Instantiate(ballPrefab);
        var collider = tempBall.GetComponent<CircleCollider2D>();
        float ballColliderRadius = collider.radius;
        spawnLocationMin = new Vector2(
            tempBall.transform.position.x - ballColliderRadius,
            tempBall.transform.position.y - ballColliderRadius);
        spawnLocationMax = new Vector2(
            tempBall.transform.position.x + ballColliderRadius,
            tempBall.transform.position.y + ballColliderRadius);
        DestroyImmediate(tempBall);
    }

    /// <summary>
    /// True if location free.
    /// </summary>
    /// <returns></returns>
    private bool IsSpawnLocationFree()
    {
        return !Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax);
    }
}
