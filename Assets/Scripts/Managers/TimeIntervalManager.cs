using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeIntervalManager : MonoBehaviour
{
    [SerializeField] private float _timeInterval;
    private float _timePassed;

    private int _intervalsCompleted;
    
    // Start is called before the first frame update
    void Start()
    {
        _intervalsCompleted = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed >= _timeInterval)
        {
            _intervalsCompleted++;
            _timePassed -= _timeInterval;
            
            EventManager.Instance.OnTimeIntervalCompleted(_intervalsCompleted);
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
}
