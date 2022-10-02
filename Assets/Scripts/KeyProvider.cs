using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyProvider : MonoBehaviour
{
    [SerializeField] private int _keyId;
    [SerializeField] private string _keyCollectDesc;
    
    private bool _isKeyCollected;

    private void OnEnable()
    {
        EventManager.Instance.CollectedKey += OnKeyCollection;
    }

    private void OnDisable()
    {
        EventManager.Instance.CollectedKey -= OnKeyCollection;
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
        Debug.Log("Trigger Enter for Key provider");
        if (other.tag.Equals("Player") && !_isKeyCollected)
        {
            // notify that key can be collected
            EventManager.Instance.OnEnteringKeyCollectArea(_keyId, _keyCollectDesc);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            EventManager.Instance.OnLeavingKeyCollectArea();
        }
    }

    private void OnKeyCollection(int id)
    {
        if (id == _keyId)
        {
            _isKeyCollected = true;
        }
    }
}
