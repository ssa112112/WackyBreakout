
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
	void Awake()
    {
        if (!ScreenUtils.IsInitialized()) ScreenUtils.Initialize();
        if (!ConfigurationUtils.IsInitialized()) ConfigurationUtils.Initialize();
        if (!AudioManager.IsInitialized()) AudioManager.Initialize();
        EventsManager.Clear();
        EndGameWaiter.RestoreGlobalState();
    }
}
