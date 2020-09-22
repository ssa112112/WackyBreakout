using UnityEngine;
using UnityEngine.UI;

public class HUG : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text ballsLeftText;

    //Support with scoring functionality 
    static int actualScore = 0;

    //Support with ballsLeft fields
    int actualBallsLeft;

    //Support with "LastBallLost" event
    LastBallLost lastBallLost = new LastBallLost();

    private void Start()
    {
        //Set starting values
        actualBallsLeft = ConfigurationUtils.AmountBalls;
        SetBallsLeft(actualBallsLeft);
        AddScore(-actualScore);
        //Become listener to add points
        EventsManager.AddPointsListener(AddScore);
        //Become listener to reduce balls left
        EventsManager.AddReduceBallsLeftListener(RemoveBallFromLeft);
        //Become invoker for LastBallLost
        EventsManager.AddLastBallLostInvoker(lastBallLost);
    }

    void AddScore(int score)
    {
        actualScore += score;
        scoreText.text = ($"Score: {actualScore}");
    }

    public void RemoveBallFromLeft()
    {
        actualBallsLeft--;
        SetBallsLeft(actualBallsLeft);
        if (actualBallsLeft == 0)
            lastBallLost.Invoke();
    }

    private void SetBallsLeft(int BallsLeft)
    {
        ballsLeftText.text = ($"BALLS LEFT: {BallsLeft}");
    }


    static public int GetCurrentScore()
    {
        return actualScore;
    }
}
