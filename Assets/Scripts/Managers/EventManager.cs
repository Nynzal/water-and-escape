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
    
    
    // ------------- Keys
    
    // Entered a collectable key area
    public event Action<int, string> EnteredKeyCollectArea;

    public void OnEnteringKeyCollectArea(int id, string collectDesc)
    {
        EnteredKeyCollectArea?.Invoke(id, collectDesc);
    }
    
    // Left collectable key area
    public event Action LeftKeyCollectArea;

    public void OnLeavingKeyCollectArea()
    {
        LeftKeyCollectArea?.Invoke();
    }
    
    // Collected Key
    public event Action<int> CollectedKey;

    public void OnKeyCollection(int id)
    {
        CollectedKey?.Invoke(id);
    }
    
    // Entered Gate Area
    public event Action<bool, string> EnteredGateArea;

    public void OnEnteringGateArea(bool key, string desc)
    {
        EnteredGateArea?.Invoke(key, desc);
    }

    // Leaving Gate Area
    public event Action LeftGateArea;

    public void OnLeavingGateArea()
    {
        LeftGateArea?.Invoke();
    }
    
    // Using Key on Gate
    public event Action<int> UsedKeyOnGate;

    public void OnUsingKeyOnGate(int id)
    {
        UsedKeyOnGate?.Invoke(id);
    }
    
    // Interaction Area
    public event Action<Vector2> EnteredInteractionArea;
    public void OnEnteringInteractionArea(Vector2 worldAnchor)
    {
        EnteredInteractionArea?.Invoke(worldAnchor);
    }

    public event Action LeftInteractionArea;
    public void OnLeavingInteractionArea()
    {
        LeftInteractionArea?.Invoke();
    }
    
    // Interaction Key
    public event Action HoldingInteractionKey;
    public void OnInteractionKeyDown()
    {
        HoldingInteractionKey?.Invoke();
    }

    public event Action ReleasedInteractionKey;
    public void OnInteractionKeyRelease()
    {
        ReleasedInteractionKey?.Invoke();
    }
}
