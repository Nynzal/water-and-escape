using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashCooldown;
    
    // Components
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    
    // State
    private bool _isTouchingWater;
    private bool _isDashTriggered;
    private bool _isDashing;
    private bool _isDashReady = true;
    
    // Interaction Timer
    [SerializeField] private float _waterGrabTime;
    private float _slurpingTime;
    
    //  ----Abilities
    // Dash
    private Vector2 _dashDirection;
    private float _dashRecoveryTimer;
    private float _dashTraveled;
    
    
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
        
        // Ability use
        if (Input.GetKeyDown(KeyCode.Space) && _isDashReady)
        {
            _isDashTriggered = true;
        }
    }

    private void FixedUpdate()
    {
        // Movement vector
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float d = Mathf.Sqrt(x * x + y * y);
        if (d > 1)
        {
            x /= d;
            y /= d;
        }
        
        // Dashing State
        if (_isDashTriggered)
        {
            _isDashReady = false;
            _isDashTriggered = false;
            _isDashing = true;
            _dashRecoveryTimer = 0;
            _dashTraveled = 0;
            _dashDirection = new Vector2(x, y);
            EventManager.Instance.OnPlayerDashUsed(_dashCooldown);
        }

        // Dash cooldown
        if (!_isDashReady)
        {
            _dashRecoveryTimer += Time.fixedDeltaTime;
            if (_dashRecoveryTimer >= _dashCooldown)
            {
                _isDashReady = true;
                Debug.Log("Dash cooldown up");
                EventManager.Instance.OnPlayerDashReady();
            }
        }
        

        if (_isDashing)
        {
            x = _dashDirection.x * _dashSpeed * Time.fixedDeltaTime;
            y = _dashDirection.y * _dashSpeed * Time.fixedDeltaTime;
            
            _dashTraveled += _dashDirection.magnitude * _dashSpeed * Time.fixedDeltaTime;

            if (_dashTraveled >= _dashDistance)
            {
                _isDashing = false;
            }
        }
        else
        {
            // Normal Movement
            x *= _movementSpeed * Time.fixedDeltaTime;
            y *= _movementSpeed * Time.fixedDeltaTime;
        }
        
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

    public void ReceiveAttack(float damage)
    {
        Debug.Log("Player got attacked hit with damage: " + damage);
        EventManager.Instance.OnPlayerReceivingDamage(damage);
    }
}
