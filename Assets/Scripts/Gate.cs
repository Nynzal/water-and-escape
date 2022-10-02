using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _gateKeyId;
    [SerializeField] private string _unlockableText;
    [SerializeField] private string _lockedText;
    
    private bool _isUnlockable;
    private bool _isUnlocked;
    
    private void OnEnable()
    {
        EventManager.Instance.UsedKeyOnGate += OnKeyUsage;
        EventManager.Instance.CollectedKey += OnKeyCollected;
    }

    private void OnDisable()
    {
        EventManager.Instance.UsedKeyOnGate -= OnKeyUsage;
        EventManager.Instance.CollectedKey -= OnKeyCollected;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (_isUnlockable)
            {
                EventManager.Instance.OnEnteringGateArea(_isUnlockable, _unlockableText);
            }
            else
            {
                EventManager.Instance.OnEnteringGateArea(_isUnlockable, _lockedText);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            EventManager.Instance.OnLeavingGateArea();
        }
    }

    private void OnKeyCollected(int id)
    {
        if (id == _gateKeyId)
        {
            _isUnlockable = true;
        }
    }
    
    private void OnKeyUsage(int id)
    {
        if (id == _gateKeyId)
        {
            _isUnlocked = true;
            gameObject.SetActive(false);
        }
    }
    
}
