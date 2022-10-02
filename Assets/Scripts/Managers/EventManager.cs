using System;
using UnityEngine;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }

            return _instance;
        }
    }
    
    
    // On touching a water source
    public event Action<bool> WaterTouchingState;

    public void OnWaterTouchingStateChange(bool newState)
    {
        WaterTouchingState?.Invoke(newState);
    }
    
    
    // On drinking water
    public event Action AccessedWater;

    public void OnAccessingWater()
    {
        AccessedWater?.Invoke();
    }
    
    
    // On passing of a time interval
    public event Action<int> TimeIntervalCompleted;

    public void OnTimeIntervalCompleted(int number)
    {
        TimeIntervalCompleted?.Invoke(number);
    }
}
