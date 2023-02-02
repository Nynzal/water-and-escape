using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _following;

    [SerializeField] private Vector2 _xWorldConstraint;
    [SerializeField] private Vector2 _yWorldConstraint;
    [SerializeField] private Vector2 _borderBuffer;

    [SerializeField] private Camera _camera;
    
    private Vector2 _xConstraints;
    private Vector2 _yConstraints;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraConstraints();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endPos = new Vector3();
        Vector3 target = _following.transform.position;
        endPos.x = target.x < _xConstraints.x ? _xConstraints.x
            : (target.x > _xConstraints.y) ? _xConstraints.y : target.x;
        endPos.y = target.y < _yConstraints.x ? _yConstraints.x
            : (target.y > _yConstraints.y) ? _yConstraints.y : target.y;
        endPos.z = transform.position.z;

        transform.position = endPos;
    }

    private void UpdateCameraConstraints()
    {
        Vector3 bottomLeft = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0));

        _borderBuffer = new Vector2((topRight.x - bottomLeft.x) / 2f, (topRight.y - bottomLeft.y) / 2f);
        
        _xConstraints = new Vector2(_xWorldConstraint.x + _borderBuffer.x, _xWorldConstraint.y - _borderBuffer.x);
        _yConstraints = new Vector2(_yWorldConstraint.x + _borderBuffer.y, _yWorldConstraint.y - _borderBuffer.y);
    }

    public void UpdateCameraConstraints(Vector2 borderBuffer)
    {
        _borderBuffer = borderBuffer;
        UpdateCameraConstraints();
    }
}
