using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntervalDisplay : MonoBehaviour
{
    [SerializeField] private Image _circularTimer;
    [SerializeField] private TextMeshProUGUI _intervalText;

    private TimeIntervalManager _intervalManager;

    private float _maxTime;
    private float _currentTime;

    [SerializeField] private string _intervalDescription;

    private void Awake()
    {
        _intervalManager = FindObjectOfType<TimeIntervalManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.TimeIntervalCompleted += OnIntervalCompletion;
        _maxTime = _intervalManager.GetIntervalLength();
        _currentTime = _intervalManager.GetCurrentTime();
    }

    private void OnDisable()
    {
        EventManager.Instance.TimeIntervalCompleted -= OnIntervalCompletion;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        _circularTimer.fillAmount = _currentTime / _maxTime;
    }

    private void OnIntervalCompletion(int number)
    {
        _currentTime = 0;
        _intervalText.text = "" + number + "\n" + _intervalDescription;
    }
}
