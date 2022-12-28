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
    [SerializeField] private Collider2D _detectionRangeCollider;
    [SerializeField] private float _aggressionRange;
    private Vector3 _startPosition;
    
    // Stats
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Player entered 
        }
    }
}
