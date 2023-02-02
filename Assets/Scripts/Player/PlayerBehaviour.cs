using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Shadow stuff
    [SerializeField] private PlayerShadowTouching _playerShadowTouch;
    
    // Movement variables
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashCooldown;
    
    // Sun & Shadow variables
    // percentage of the burn grace time that starts adding sun burn effect
    [SerializeField] private float _visualBurnGraceTimePercentage; 
    // time until player starts burning
    [SerializeField] private float _burnGraceTime; 
    // burn per second
    [SerializeField] private float _burnDamage;
    // burn damage fraction increase by sun intensity
    [SerializeField] private float _burnDamageFractionIncrease;
    // sun intensity until burn damage increases steeper
    [SerializeField] private int _burnDamageThreshold;
    // burn damage modifer on low burn damage threshold
    [SerializeField] private float _burnDamageLowModifier;
    // Time at which grace period reduction reaches maximum (length of animation curve)
    [SerializeField] private float _burnIntensityCurveLength;
    // Curve determining the flow of grace period reduction
    [SerializeField] private AnimationCurve _burnIntensityCurve;
    

    private float _currentTimeInSun;
    
    
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
        Move();
        
        
        // Handle being in the shadow
        if (_playerShadowTouch.GetPlayerInShadow())
        {
            // Reset burn timer stuff
            if (_currentTimeInSun > 0)
            {
                _currentTimeInSun -= Time.fixedDeltaTime;
                if (_currentTimeInSun < 0)
                {
                    _currentTimeInSun = 0;
                }
                
                SetSunCameraEffectIntensity();
            }
        }
        else
        {
            // In the sun stuff
            InSunHandling();
        }
    }

    public void ReceiveAttack(float damage)
    {
        EventManager.Instance.OnPlayerReceivingDamage(damage);
    }

    private void Move()
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
    }

    private void InSunHandling()
    {
        // get current sun factor
        float graceTimeSunFactor = _burnIntensityCurve.Evaluate(Sun.sunIntensity / _burnIntensityCurveLength);

        // add time based on the grace time reduction through sun intensity
        _currentTimeInSun += Time.fixedDeltaTime / graceTimeSunFactor;
        if (_currentTimeInSun >= _burnGraceTime)
        {
            _currentTimeInSun = _burnGraceTime;
            
            // sun damage effect
            EventManager.Instance.OnPlayerReceivingDamage(CalculateSunDamage());
        }
        
        // set the current camera effect intensity
        SetSunCameraEffectIntensity();
    }

    private void SetSunCameraEffectIntensity()
    {
        float effectThreshold = _burnGraceTime * _visualBurnGraceTimePercentage;
        if (_currentTimeInSun > effectThreshold)
        {
            Sun.sunBurnActive = (_currentTimeInSun - effectThreshold) / (_burnGraceTime - effectThreshold);
        }
        else
        {
            Sun.sunBurnActive = 0;
        }
    }

    private float CalculateSunDamage()
    {
        float lowerThresholdDamage = (Math.Min(Sun.sunIntensity, _burnDamageThreshold) - 1) *
                                     _burnDamageFractionIncrease * _burnDamage *
                                     _burnDamageLowModifier;
        float highThresholdDamage = Math.Max(Sun.sunIntensity - _burnDamageThreshold, 0) * _burnDamageFractionIncrease *
                                    _burnDamage;
        float result = (_burnDamage + lowerThresholdDamage + highThresholdDamage) * Time.fixedDeltaTime;
        return result;
    }
}
