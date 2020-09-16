using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsManager
{
    #region Fields

    //Support with freezerEffect
    static UnityAction<float> freezerEffectListener;
    static List<FreezerEffectActivated> freezerEffectInvokers = new List<FreezerEffectActivated>();

    //Support with speedupEffect
    static UnityAction<float, float> speedupEffectListener;
    static List<SpeedupEffectActivated> speedupEffectInvokers = new List<SpeedupEffectActivated>();

    //Support with pauseCanceled
    static List<UnityAction> pauseCanceledListeners = new List<UnityAction>();
    static List<PauseCanceled> pauseCanceledInvokers = new List<PauseCanceled>();

    //Support with pauseCanceled
    static List<UnityAction<int>> addPointsListeners = new List<UnityAction<int>>();
    static List<AddPoints> addPointsInvokers = new List<AddPoints>();

    //Support with reduce balls left
    static List<UnityAction> reduceBallsLeftListeners = new List<UnityAction>();
    static List<ReduceBallsLeft> reduceBallsLeftInvokers = new List<ReduceBallsLeft>();

    //Support with respawn balls
    static List<UnityAction> disappearingBallListeners = new List<UnityAction>();
    static List<DisappearingBall> disappearingBallInvokers = new List<DisappearingBall>();

    //Support with event: "LastBallLost"
    static List<UnityAction> lastBallLostListeners = new List<UnityAction>();
    static List<LastBallLost> lastBallLostInvokers = new List<LastBallLost>();

    //Support with check destroed blocks
    static List<UnityAction> blockDestroyedListeners = new List<UnityAction>();
    static List<BlockDestroyed> blockDestroyedInvokers = new List<BlockDestroyed>();

    #endregion

    #region FreezerEffect

    public static void AddFreezerEffectListener(UnityAction<float> action)
    {
        freezerEffectListener = action;
        foreach (var currentEvent in freezerEffectInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddFreezerEffectInvoker(FreezerEffectActivated inputEvent)
    {
        freezerEffectInvokers.Add(inputEvent);
        if (freezerEffectListener != null)
            inputEvent.AddListener(freezerEffectListener);
    }

    #endregion

    #region SpeedupEffect
    public static void AddSpeedupEffectListener(UnityAction<float,float> action)
    {
        speedupEffectListener = action;
        foreach (var currentEvent in speedupEffectInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddSpeedupEffectInvoker(SpeedupEffectActivated inputEvent)
    {
        speedupEffectInvokers.Add(inputEvent);
        if (speedupEffectListener != null)
            inputEvent.AddListener(speedupEffectListener);
    }

    #endregion

    #region PauseCanceled

    public static void AddPauseCanceledListener(UnityAction action)
    {
        pauseCanceledListeners.Add(action);
        foreach (var pauseCanceledInvoker in pauseCanceledInvokers)
            pauseCanceledInvoker.AddListener(action);
    }

    public static void AddPauseCanceledInvoker(PauseCanceled inputEvent)
    {
        pauseCanceledInvokers.Add(inputEvent);
        foreach (var pauseCanceledListener in pauseCanceledListeners)
            inputEvent.AddListener(pauseCanceledListener);
    }

    public static void RemovePauseCanceledListener(UnityAction action)
    {
        pauseCanceledListeners.Remove(action);
        foreach (var pauseCanceledInvoker in pauseCanceledInvokers)
            pauseCanceledInvoker.RemoveListener(action);
    }

    #endregion

    #region AddPoints

    public static void AddPointsListener(UnityAction<int> action)
    {
        addPointsListeners.Add(action);
        foreach (var currentEvent in addPointsInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddPointsInvoker(AddPoints inputEvent)
    {
        addPointsInvokers.Add(inputEvent);
        foreach (var currentAction in addPointsListeners)
            inputEvent.AddListener(currentAction);
    }

    #endregion

    #region ReduceBallsLeft

    public static void AddReduceBallsLeftListener(UnityAction action)
    {
        reduceBallsLeftListeners.Add(action);
        foreach (var currentEvent in reduceBallsLeftInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddReduceBallsLeftInvoker(ReduceBallsLeft inputEvent)
    {
        reduceBallsLeftInvokers.Add(inputEvent);
        foreach (var currentAction in reduceBallsLeftListeners)
            inputEvent.AddListener(currentAction);
    }

    #endregion

    #region DisappearingBall

    public static void AddDisappearingBallListener(UnityAction action)
    {
        disappearingBallListeners.Add(action);
        foreach (var currentEvent in disappearingBallInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddDisappearingBallInvoker(DisappearingBall inputEvent)
    {
        disappearingBallInvokers.Add(inputEvent);
        foreach (var currentAction in disappearingBallListeners)
            inputEvent.AddListener(currentAction);
    }

    #endregion

    #region LastBallLost

    public static void AddLastBallLostListener(UnityAction action)
    {
        lastBallLostListeners.Add(action);
        foreach (var currentEvent in lastBallLostInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddLastBallLostInvoker(LastBallLost inputEvent)
    {
        lastBallLostInvokers.Add(inputEvent);
        foreach (var currentAction in lastBallLostListeners)
            inputEvent.AddListener(currentAction);
    }

    #endregion

    #region BlockDestroyed

    public static void AddBlockDestroyedListener(UnityAction action)
    {
        blockDestroyedListeners.Add(action);
        foreach (var currentEvent in blockDestroyedInvokers)
            currentEvent.AddListener(action);
    }

    public static void AddBlockDestroyedInvoker(BlockDestroyed inputEvent)
    {
        blockDestroyedInvokers.Add(inputEvent);
        foreach (var currentAction in blockDestroyedListeners)
            inputEvent.AddListener(currentAction);
    }

    #endregion

    #region Clear

    /// <summary>
    /// Delete all references
    /// </summary>
    public static void Clear()
    {
        freezerEffectListener = null;
        freezerEffectInvokers.Clear();
        speedupEffectListener = null;
        speedupEffectInvokers.Clear();
        pauseCanceledListeners.Clear();
        pauseCanceledInvokers.Clear();
        addPointsListeners.Clear();
        addPointsInvokers.Clear();
        reduceBallsLeftListeners.Clear();
        reduceBallsLeftInvokers.Clear();
        disappearingBallListeners.Clear();
        disappearingBallInvokers.Clear();
        lastBallLostListeners.Clear();
        lastBallLostInvokers.Clear();
        blockDestroyedListeners.Clear();
        blockDestroyedInvokers.Clear();
    }
}

#endregion