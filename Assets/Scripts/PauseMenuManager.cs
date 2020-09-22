using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Control pause menu
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    //Support with pausing
    float savedTimeScale;

    PauseCanceled pauseCanceled = new PauseCanceled();

    private void Start()
    {
        //Save current timeScale
        savedTimeScale = Time.timeScale;
        //And stop time
        Time.timeScale = 0;
        //Become invoker
        EventsManager.AddPauseCanceledInvoker(pauseCanceled);
        //Become listener
        EventsManager.AddPauseCanceledListener(PauseCanceled);
    }

    /// <summary>
    /// Go to a menu item
    /// </summary>
    /// <param name="name"></param>
    private void GoToMenu(MenuName name)
    {
        AudioManager.Play(AudioName.Сlick);
        switch (name)
        {
            case MenuName.MainMenu:
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            case MenuName.Resume:
                //Restore timeScale
                Time.timeScale = savedTimeScale;
                //Unsubscribe
                EventsManager.RemovePauseCanceledListener(PauseCanceled);
                //And destroy PauseMenu sceen
                SceneManager.UnloadSceneAsync("PauseMenu");
                break;
        }
    }

    public void MainMenu()
    {
        GoToMenu(MenuName.MainMenu);
    }

    public void Resume()
    {
        pauseCanceled.Invoke();
        GoToMenu(MenuName.Resume);
    }

    void PauseCanceled()
    {
        GoToMenu(MenuName.Resume);
    }
}
