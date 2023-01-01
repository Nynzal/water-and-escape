using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private float _maxWaterCapacity;
    [SerializeField] private float _currentWater;
    [SerializeField] private float _waterConsumption;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.AccessedWater += OnWaterPickup;
        EventManager.Instance.PlayerReceivedDamage += OnPlayerDamage;
    }

    private void OnDisable()
    {
        EventManager.Instance.AccessedWater -= OnWaterPickup;
        EventManager.Instance.PlayerReceivedDamage -= OnPlayerDamage;
    }

    // Update is called once per frame
    void Update()
    {
        // Water decrease
        _currentWater -= _waterConsumption * Time.deltaTime;
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
}
