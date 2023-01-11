using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField] private Image _cooldownOverlay;
    private float _totalCooldown;
    private float _currentCooldown;
    
    private void OnEnable()
    {
        EventManager.Instance.PlayerDashTriggered += OnPlayerDashUsed;
        EventManager.Instance.PlayerDashReady += OnPlayerDashReady;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerDashTriggered -= OnPlayerDashUsed;
        EventManager.Instance.PlayerDashReady -= OnPlayerDashReady;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentCooldown >= 0)
        {
            _currentCooldown -= Time.deltaTime;
            _cooldownOverlay.fillAmount = _currentCooldown / _totalCooldown;
        }
    }

    private void OnPlayerDashUsed(float cooldown)
    {
        _totalCooldown = cooldown;
        _currentCooldown = cooldown;
    }

    private void OnPlayerDashReady()
    {
        _currentCooldown = 0;
    }
}
