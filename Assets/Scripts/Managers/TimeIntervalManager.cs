using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeIntervalManager : MonoBehaviour
{
    private bool _isRunning;
    
    [SerializeField] private float _timeInterval;
    private float _timePassed;

    private int _intervalsCompleted;

    private void OnEnable()
    {
        EventManager.Instance.GameSpeedState += OnGameSpeedChange;
    }

    private void OnDisable()
    {
        EventManager.Instance.GameSpeedState -= OnGameSpeedChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        _intervalsCompleted = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isRunning)
        {
            return;
        }
        
        _timePassed += Time.deltaTime;
        if (_timePassed >= _timeInterval)
        {
            _intervalsCompleted++;
            _timePassed -= _timeInterval;
            
            EventManager.Instance.OnTimeIntervalCompleted(_intervalsCompleted);
            Sun.sunIntensity++;
        }
    }

    public float GetCurrentTime()
    {
        return _timePassed;
    }

    public float GetIntervalLength()
    {
        return _timeInterval;
    }

    private void OnGameSpeedChange(bool running)
    {
        _isRunning = running;
    }
}
