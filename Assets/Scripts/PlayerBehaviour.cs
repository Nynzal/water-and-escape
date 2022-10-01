using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float d = Mathf.Sqrt(x * x + y * y);
        Debug.Log("movement vector length: " + d);
        if (d > 1)
        {
            x /= d;
            y /= d;
        }
        x *= _movementSpeed * Time.fixedDeltaTime;
        y *= _movementSpeed * Time.fixedDeltaTime;
        
        _rigidbody2D.MovePosition(transform.position + new Vector3(x, y, 0));
    }
}
