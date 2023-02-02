using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private float _maxWaterCapacity;
    [SerializeField] private float _currentWater;
    [SerializeField] private float _waterConsumption;

    private bool _isRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.AccessedWater += OnWaterPickup;
        EventManager.Instance.PlayerReceivedDamage += OnPlayerDamage;
        EventManager.Instance.GameSpeedState += OnGameSpeedChange;
    }

    private void OnDisable()
    {
        EventManager.Instance.AccessedWater -= OnWaterPickup;
        EventManager.Instance.PlayerReceivedDamage -= OnPlayerDamage;
        EventManager.Instance.GameSpeedState -= OnGameSpeedChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isRunning)
        {
            return;
        }
        // Water decrease
        _currentWater -= _waterConsumption * Time.deltaTime;

        if (_currentWater <= 0)
        {
            EventManager.Instance.OnGameSpeedChange(false);
            EventManager.Instance.OnGameOver();
        }
    }

    private void OnWaterPickup(WaterSource waterSource)
    {
        float gainedWater = waterSource.TakeWater(_maxWaterCapacity - _currentWater);
        _currentWater += gainedWater;
    }

    public float GetCurrentWater()
    {
        return _currentWater;
    }

    public float GetMaxWater()
    {
        return _maxWaterCapacity;
    }


    private void OnPlayerDamage(float damage)
    {
        _currentWater -= damage;
    }


    private void OnGameSpeedChange(bool running)
    {
        _isRunning = running;
    }
}
