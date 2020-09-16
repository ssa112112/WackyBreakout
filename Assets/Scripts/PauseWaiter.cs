using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Wait pause button and activate pause, if need
/// </summary>
public class PauseWaiter : MonoBehaviour
{
    static bool pauseMod;

    PauseCanceled pauseCanceled = new PauseCanceled();

    private void Start()
    {
        pauseMod = false;
        //Become invoker
        EventsManager.AddPauseCanceledInvoker(pauseCanceled);
        //Become listener
        EventsManager.AddPauseCanceledListener(PauseCanceled);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (pauseMod == false)
            {
                pauseMod = true;
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            }
            else
            {
                pauseCanceled.Invoke();
            }
    }

    void PauseCanceled()
    {
        pauseMod = false;
    }

    /// <summary>
    /// True if pause mod active
    /// </summary>
    /// <returns></returns>
    public static bool IsPauseMod()
    {
        return pauseMod;
    }
}
