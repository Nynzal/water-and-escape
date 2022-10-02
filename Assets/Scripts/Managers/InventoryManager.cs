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
    }

    private void OnDisable()
    {
        EventManager.Instance.AccessedWater -= OnWaterPickup;
    }

    // Update is called once per frame
    void Update()
    {
        // Water decrease
        _currentWater -= _waterConsumption * Time.deltaTime;
    }

    private void OnWaterPickup()
    {
        _currentWater = _maxWaterCapacity;
    }

    public float GetCurrentWater()
    {
        return _currentWater;
    }

    public float GetMaxWater()
    {
        return _maxWaterCapacity;
    }
}
