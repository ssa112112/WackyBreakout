using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play all audios in the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    //Info anout initialize status
    static bool initialized = false;

    //Support with play aydios
    static AudioSource audioSource;
    static Dictionary<AudioName,AudioClip> audioClips = new Dictionary<AudioName, AudioClip>();
    static string nameOfFolderWithAudio = "Audio";

    static public void Initialize()
    {
        //Create object
        var audioManagerObject = new GameObject("AudioManager");
        //Don't destroy its
        DontDestroyOnLoad(audioManagerObject);
        //Create audio source
        audioSource = audioManagerObject.AddComponent<AudioSource>();
        //Download all audio clips
        foreach (string audioName in Enum.GetNames(typeof(AudioName)))
        {
            audioClips.Add((AudioName)Enum.Parse(typeof(AudioName), audioName),
                Resources.Load<AudioClip>(nameOfFolderWithAudio+"/"+ audioName));
        }
        //Set flag
        initialized = true;
    }
    
    /// <summary>
    /// Play the audio clip with given name
    /// </summary>
    /// <param name="audioName"></param>
    public static void Play(AudioName audioName)
    {
        audioSource.PlayOneShot(audioClips[audioName]);
    }

    /// <summary>
    /// True if AudioManager initialized
    /// </summary>
    /// <returns></returns>
    public static bool IsInitialized()
    {
        return initialized;
    }
}