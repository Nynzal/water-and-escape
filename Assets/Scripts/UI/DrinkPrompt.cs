using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPrompt : MonoBehaviour
{
    [SerializeField] private GameObject _promptVisuals;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.WaterTouchingState += OnWaterStateChange;
    }

    private void OnDisable()
    {
        EventManager.Instance.WaterTouchingState -= OnWaterStateChange;
    }

    // When the player enters a water source
    private void OnWaterStateChange(bool state)
    {
        _promptVisuals.SetActive(state);
    }
}
