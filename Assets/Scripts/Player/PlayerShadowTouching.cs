using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowTouching : MonoBehaviour
{
    private bool _isTouchingShadow;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private int _shadowLayer;

    private void FixedUpdate()
    {
        // check if currently touching water
        int layer = 1 << _shadowLayer;
        if (_collider2D.IsTouchingLayers(layer))
        {
            if (!_isTouchingShadow)
            {
                _isTouchingShadow = true;
                EventManager.Instance.OnPlayerInShadowStateChange(_isTouchingShadow);
            }
        }
        else
        {
            if (_isTouchingShadow)
            {
                _isTouchingShadow = false;
                EventManager.Instance.OnPlayerInShadowStateChange(_isTouchingShadow);
            }
            
        }
    }

    public bool GetPlayerInShadow()
    {
        return _isTouchingShadow;
    }
}
