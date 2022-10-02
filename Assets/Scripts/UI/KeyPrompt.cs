using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPrompt : MonoBehaviour
{
    [SerializeField] private GameObject _promptVisuals;
    [SerializeField] private TextMeshProUGUI _promptText;
    
    private bool _isInProviderArea;
    private bool _isInGateArea;
    
    private void OnEnable()
    {
        EventManager.Instance.EnteredKeyCollectArea += OnEnteringKeyArea;
        EventManager.Instance.LeftKeyCollectArea += OnLeavingKeyArea;
        EventManager.Instance.CollectedKey += OnKeyCollected;
        EventManager.Instance.EnteredGateArea += OnEnteringGateArea;
        EventManager.Instance.LeftGateArea += OnLeavingGateArea;
        EventManager.Instance.UsedKeyOnGate += OnUsingKey;
    }

    private void OnDisable()
    {
        EventManager.Instance.EnteredKeyCollectArea -= OnEnteringKeyArea;
        EventManager.Instance.LeftKeyCollectArea -= OnLeavingKeyArea;
        EventManager.Instance.CollectedKey -= OnKeyCollected;
        EventManager.Instance.EnteredGateArea -= OnEnteringGateArea;
        EventManager.Instance.LeftGateArea -= OnLeavingGateArea;
        EventManager.Instance.UsedKeyOnGate -= OnUsingKey;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnteringKeyArea(int id, string desc)
    {
        _promptText.text = desc;
        _promptVisuals.SetActive(true);
    }

    private void OnLeavingKeyArea()
    {
        _promptVisuals.SetActive(false);
    }

    private void OnKeyCollected(int id)
    {
        _promptVisuals.SetActive(false);
    }

    private void OnEnteringGateArea(bool key, string desc)
    {
        _promptText.text = desc;
        _promptVisuals.SetActive(true);
    }

    private void OnLeavingGateArea()
    {
        _promptVisuals.SetActive(false);
    }

    private void OnUsingKey(int id)
    {
        _promptVisuals.SetActive(false);
    }
}
