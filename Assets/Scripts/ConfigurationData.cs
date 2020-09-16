using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Globalization;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData : MonoBehaviour
{
    /// <summary>
    /// Try to download settings, but in case error use default value.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        try
        {
            //Game pause, until download
            Time.timeScale = 0;
            //And load setting
            StartCoroutine(LoadSettings(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName)));
        }
        catch (Exception e)
        {
            //Say, if error
            Debug.Log("Load settings isn't done");
            Debug.Log(e.Message);
        }
        finally
        {
            //Always start game
            Time.timeScale = 1;
        }
    }

    #region Fields

    const string ConfigurationDataFileName = "settings.csv";

    //Default configuration data
    
    float paddleMoveUnitsPerSecond = 12;
    float ballImpulseForce = 200;
    float ballLifeTime = 15;
    float timeBeforeBallStartMoving = 1;
    float spawnTimeMin = 9;
    float spawnTimeMax = 15;
    float standartBlockPoints = 5;
    float amountRow = 3;
    float bonusBlockPoints = 25;
    float freezerBlockPoints = 10;
    float speedupBlockPoints = 10;
    float standartBlockProbability = 0.7f;
    float freezertBlockProbability = 0.1f;
    float speedupBlockProbability = 0.1f;
    float bonusBlockProbability = 0.2f;
    float amountBalls = 9;
    float freezerEffectDuration = 2f;
    float spedupEffectDuration = 2f;
    float speedupEffectForce = 2f;
    float respawnTimeMin = 0.5f;
    float respawnTimeMax = 2f;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }

    public float BallLifeTime
    {
        get { return ballLifeTime; }
    }

    public float TimeBeforeBallStartMoving
    {
        get { return timeBeforeBallStartMoving; }
    }

    public float SpawnTimeMin
    {
        get { return spawnTimeMin; }
    }

    public float SpawnTimeMax
    {
        get { return spawnTimeMax; }
    }

    public int StandartBlockPoints
    {
        get { return (int)standartBlockPoints;  }
    }

    public int AmountRow
    {
        get { return (int)amountRow; }
    }
    public int BonusBlockPoints
    {
        get { return (int)bonusBlockPoints; }
    }

    public int FreezerBlockPoints
    {
        get { return (int)freezerBlockPoints; }
    }

    public int SpeedupBlockPoints
    {
        get { return (int)speedupBlockPoints; }
    }

    public float StandartBlockProbability
    {
        get { return standartBlockProbability; }
    }

    public float FreezertBlockProbability
    {
        get { return freezertBlockProbability; }
    }

    public float SpeedupBlockProbability
    {
        get { return speedupBlockProbability; }
    }

    public float BonusBlockProbability
    {
        get { return bonusBlockProbability; }
    }

    public int AmountBalls
    {
        get { return (int)amountBalls; }
    }

    public float FreezerEffectDuration
    {
        get { return freezerEffectDuration; }
    }

    public float SpedupEffectDuration
    {
        get { return spedupEffectDuration; }
    }

    public float SpeedupEffectForce
    {
        get { return speedupEffectForce; }
    }

    public float RespawnTimeMin
    {
        get { return respawnTimeMin; }
    }
    public float RespawnTimeMax
    {
        get { return respawnTimeMax; }
    }
    #endregion

    #region Method

    /// <summary>
    /// Set value
    /// </summary>
    /// <param name="csvValue"></param>
    private void SetConfigurationDataFields(string csvValue)
    {
        //Convert
        float[] floatValue = 
            Array.ConvertAll(csvValue.Split(','), delegate (string s) { return float.Parse(s,CultureInfo.InvariantCulture); });
        //Set
        paddleMoveUnitsPerSecond = floatValue[0];
        ballImpulseForce = floatValue[1];
        ballLifeTime = floatValue[2];
        timeBeforeBallStartMoving = floatValue[3];
        spawnTimeMin = floatValue[4];
        spawnTimeMax = floatValue[5];
        standartBlockPoints = floatValue[6];
        amountRow = floatValue[7];
        bonusBlockPoints = floatValue[8];
        freezerBlockPoints = floatValue[9];
        speedupBlockPoints = floatValue[10];
        standartBlockProbability = floatValue[11];
        freezertBlockProbability = floatValue[12];
        speedupBlockProbability = floatValue[13];
        bonusBlockProbability = floatValue[14];
        amountBalls = floatValue[15];
        freezerEffectDuration = floatValue[16];
        spedupEffectDuration = floatValue[17];
        speedupEffectForce = floatValue[18];
        respawnTimeMin = floatValue[19];
        respawnTimeMax = floatValue[20];
    }

    /// <summary>
    /// Load setting from WWW. It work in standalone and in WEBGL
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    IEnumerator LoadSettings(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait
            yield return webRequest.SendWebRequest();
            // Get file of settings
            var fullSettingsFille = webRequest.downloadHandler.text;
            // Set settings
            SetConfigurationDataFields(fullSettingsFille.Substring(fullSettingsFille.IndexOf("\n") + 1));
            Debug.Log("Load settings success done");
        }
    }
    #endregion
}
