using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /**
     * The basic enemy works by:
     * - When player enters detection range, make a raycast every second to determine if there is a sightline
     * - If player is in sight chase them
     * - When player moves out of aggression range, return to original position
     */

    // Aggro and following
    // When first chasing can start
    [SerializeField] private Collider2D _visionRangeCollider;
    // How far away the ray cast can go / is out of sight and turns back
    [SerializeField] private float _visionRange;
    // Max distance to start position when chasing
    [SerializeField] private float _aggressionRange;
    private Vector3 _startPosition;

    private Transform _playerTransform;
    private Vector3 _lastPlayerPosition;
    
    // Stats
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _scanFrequency;
    
    
    // States
    private bool _isPlayerInRange = false;
    private bool _isPlayerVisible = false;
    private float _currentTimeInterval;

    private Rigidbody2D _rigidbody2D;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _lastPlayerPosition = _startPosition;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update enemy state 
        if (!_isPlayerInRange)
        {
            if (Vector3.Distance(transform.position, _startPosition) > 0.1f)
            {
                // Move back to start position
                MoveTo(_startPosition);
            }

            return;
        }
        
        // In a time interval, check if player is visible
        _currentTimeInterval += Time.deltaTime;
        if (_currentTimeInterval >= 1 / _scanFrequency)
        {
            _currentTimeInterval = 0;
            // Check if can see the player
            _isPlayerVisible = IsPlayerVisible();
        }

        if (_isPlayerVisible)
        {
            MoveTo(_playerTransform.position);
            return;
        }

        if (Vector3.Distance(transform.position, _lastPlayerPosition) < 0.1f)
        {
            _lastPlayerPosition = _startPosition;
        }
        
        MoveTo(_lastPlayerPosition);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Player entered 
            _playerTransform = col.transform;
            _isPlayerInRange = true;

            Debug.Log("Player entered enemy range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = null;
            _isPlayerInRange = false;
            _lastPlayerPosition = _startPosition;
            
            Debug.Log("Player left enemy range");
        }
    }

    private bool IsPlayerVisible()
    {
        /*
         Should not be necessary, when player is in range, it should have this transform set
        if (_playerTransform == null)
        {
            return false;
        }
        */

        Vector3 pos = transform.position;
        Vector3 playerDirection = (_playerTransform.position - pos).normalized;
        RaycastHit2D hit2D = Physics2D.Raycast(pos, playerDirection, _visionRange, 1 << 6);

        if (!hit2D || !hit2D.collider.CompareTag("Player"))
        {
            return false;
        }
        
        Debug.Log("Player in vision");
        _lastPlayerPosition = hit2D.transform.position;
        
        return true;
    }

    private void MoveTo(Vector3 target)
    {
        Vector3 pos = transform.position;
        // Get directional vector to target
        Vector3 step = (target - pos).normalized;

        // scale with speed and time
        step *= _speed * Time.deltaTime;

        // Move
        _rigidbody2D.MovePosition(step + pos);
    }
}
