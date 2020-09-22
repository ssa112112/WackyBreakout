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
                StartGame(isGearLevel: true);
                break;
            case MenuName.Play:
                StartGame(isGearLevel: false);
                break;
            case MenuName.Restart:
                StartGame();
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

    /// <summary>
    /// Doesn't change the parameter "isGearLevel"
    /// </summary>
    private void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    private void StartGame(bool isGearLevel)
    {
        LevelBuilder.IsGearLevel = isGearLevel;
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
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
    public void Restart()
    {
        GoToMenu(MenuName.Restart);
    }
}
