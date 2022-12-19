using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    // Current anchor in the world
    private Vector2 _worldPos;
    private Camera _camera;
    
    // State 
    private bool _isActive = false;

    private void OnEnable()
    {
        EventManager.Instance.HoldingInteractionKey += OnInteractionKey;
        EventManager.Instance.ReleasedInteractionKey += OnInteractionKeyRelease;
    }

    private void OnDisable()
    {
        EventManager.Instance.HoldingInteractionKey -= OnInteractionKey;
        EventManager.Instance.ReleasedInteractionKey -= OnInteractionKeyRelease;
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            transform.position = _camera.WorldToScreenPoint(_worldPos);
        }
    }
    

    private void OnInteractionKey()
    {
        
    }

    private void OnInteractionKeyRelease()
    {
        
    }
}
