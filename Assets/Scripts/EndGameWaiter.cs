using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Check amount blocks and listen "LastBallLost" for end game at needly moment
/// </summary>
public class EndGameWaiter : MonoBehaviour
{
    //Global info about game's status
    static bool gameOver;
    static bool won;

    //Support with calculating blocks in the scene
    static int blocksLeft;

    void Start()
    {
        //Become listener to LastBallLost
        EventsManager.AddLastBallLostListener(HandlerLastBallLost);
        //Become listner to BlockDestroyed
        EventsManager.AddBlockDestroyedListener(HandlerBlockDestroyed);
        //Set blocksLeft
        blocksLeft = LevelBuilder.AmountBlocksInStart;
    }

    void HandlerLastBallLost()
    {
        GameOver(win: false);
    }

    void GameOver(bool win)
    {
        gameOver = true;
        //Save result in static field
        won = win;

        //Play sound
        if (win)
            AudioManager.Play(AudioName.GameWon);
        else
            AudioManager.Play(AudioName.GameLost);

        //Stop time
        Time.timeScale = 0;
        //Loaf GameOver scene
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    void HandlerBlockDestroyed()
    {
        blocksLeft--;
        if (blocksLeft == 0)
            GameOver(win: true);
    }

    /// <summary>
    /// True if the player won
    /// </summary>
    /// <returns></returns>
    public static bool IsWon()
    {
        return won;
    }

    /// <summary>
    /// True if the game is over
    /// </summary>
    /// <returns></returns>
    public static bool IsGameOver()
    {
        return gameOver;
    }
}
