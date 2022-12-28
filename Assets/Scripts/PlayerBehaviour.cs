using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float _movementSpeed;

    // Components
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    
    // State
    private bool _isTouchingWater;
    
    // Interaction Timer
    [SerializeField] private float _waterGrabTime;
    private float _slurpingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();

        _isTouchingWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for water slurping input
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager.Instance.OnInteractionKeyDown();

        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            EventManager.Instance.OnInteractionKeyRelease();
        }
    }

    private void FixedUpdate()
    {
        // Movement of character
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float d = Mathf.Sqrt(x * x + y * y);
        if (d > 1)
        {
            x /= d;
            y /= d;
        }
        x *= _movementSpeed * Time.fixedDeltaTime;
        y *= _movementSpeed * Time.fixedDeltaTime;
        
        _rigidbody2D.MovePosition(transform.position + new Vector3(x, y, 0));
        
        
        // check if currently touching water
        int layer = 1 << 4;
        if (_collider2D.IsTouchingLayers(layer))
        {
            if (!_isTouchingWater)
            {
                _isTouchingWater = true;
                EventManager.Instance.OnWaterTouchingStateChange(_isTouchingWater);
            }
        }
        else
        {
            if (_isTouchingWater)
            {
                _isTouchingWater = false;
                EventManager.Instance.OnWaterTouchingStateChange(_isTouchingWater);
            }
        }
    }
}
