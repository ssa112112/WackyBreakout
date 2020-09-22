using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Menu navigation
/// </summary>
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Go to a menu item
    /// </summary>
    /// <param name="name"></param>
    private void GoToMenu(MenuName name)
    {
        AudioManager.Play(AudioName.Сlick);
        switch (name)
        {
            case MenuName.GearGamesLevel:
                break;
            case MenuName.Play:
                Time.timeScale = 1;
                SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
                break;
            case MenuName.Help:
                SceneManager.LoadScene("HelpMenu", LoadSceneMode.Single);
                break;
            case MenuName.Exit:
#if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
                Application.Quit();
#elif (UNITY_WEBGL)
                Application.OpenURL("about:blank");
#endif
                break;
            case MenuName.MainMenu:
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
        } 
    }

    public void Exit()
    {
        GoToMenu(MenuName.Exit);
    }
    public void Help()
    {
        GoToMenu(MenuName.Help);
    }
    public void Gear()
    {
        GoToMenu(MenuName.GearGamesLevel);
    }
    public void Play()
    {
        GoToMenu(MenuName.Play);
    }
    public void MainMenu()
    {
        GoToMenu(MenuName.MainMenu);
    }
}
