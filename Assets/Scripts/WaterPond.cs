using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPond : MonoBehaviour
{
    [SerializeField] private GameObject[] _waterBubbles;
    [SerializeField] private int[] _lockIntervals;
    
    private int _currentInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.TimeIntervalCompleted += IntervalUpdate;
    }

    private void OnDisable()
    {
        EventManager.Instance.TimeIntervalCompleted -= IntervalUpdate;
    }

    private void IntervalUpdate(int number)
    {
        _currentInterval = number;
        for (int i = 0; i < _lockIntervals.Length; i++)
        {
            if (_lockIntervals[i] == number)
            {
                _waterBubbles[i].SetActive(false);
            }
        }
    }
}
