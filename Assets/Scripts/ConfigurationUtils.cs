
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Fields

    static ConfigurationData configurationData;
    static bool initialized = false;

    #endregion

    #region Properties

    public static float PaddleMoveUnitsPerSecond
    {
        get { return configurationData.PaddleMoveUnitsPerSecond; }
    }

    public static float BallImpulseForce
    {
        get { return configurationData.BallImpulseForce; }
    }

    public static float BallLifeTime
    {
        get { return configurationData.BallLifeTime; }
    }
    
    public static float TimeBeforeBallStartMoving
    {
        get { return configurationData.TimeBeforeBallStartMoving; }
    }

    public static float SpawnTimeMin
    {
        get { return configurationData.SpawnTimeMin; }
    }

    public static float SpawnTimeMax
    {
        get { return configurationData.SpawnTimeMax; }
    }

    public static int StandartBlockPoints
    {
        get { return configurationData.StandartBlockPoints; }
    }

    public static int BonusBlockPoints
    {
        get { return configurationData.BonusBlockPoints; }
    }

    public static int FreezerBlockPoints
    {
        get { return configurationData.FreezerBlockPoints; }
    }

    public static int SpeedupBlockPoints
    {
        get { return configurationData.SpeedupBlockPoints; }
    }

    public static int AmountRow
    {
        get { return configurationData.AmountRow; }
    }

    public static float StandartBlockProbability
    {
        get { return configurationData.StandartBlockProbability; }
    }

    public static float FreezertBlockProbability
    {
        get { return configurationData.FreezertBlockProbability; }
    }

    public static float SpeedupBlockProbability
    {
        get { return configurationData.SpeedupBlockProbability; }
    }

    public static float BonusBlockProbability
    {
        get { return configurationData.BonusBlockProbability; }
    }
    
    public static int AmountBalls
    {
        get { return configurationData.AmountBalls; }
    }

    public static float FreezerEffectDuration
    {
        get { return configurationData.FreezerEffectDuration; }
    }

    public static float SpedupEffectDuration
    {
        get { return configurationData.SpedupEffectDuration; }
    }

    public static float SpeedupEffectForce
    {
        get { return configurationData.SpeedupEffectForce; }
    }

    public static float RespawnTimeMin
    {
        get { return configurationData.RespawnTimeMin; }
    }
    public static float RespawnTimeMax
    {
        get { return configurationData.RespawnTimeMax; }
    }
    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        var configurationDataObject = new GameObject("ConfigurationDataObject");
        configurationData = configurationDataObject.AddComponent<ConfigurationData>();
        initialized = true;
    }

    /// <summary>
    /// True if utils initialized
    /// </summary>
    /// <returns></returns>
    public static bool IsInitialized()
    {
        return initialized;
    }
}
