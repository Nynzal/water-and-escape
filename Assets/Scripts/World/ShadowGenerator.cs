using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGenerator : MonoBehaviour
{
    [SerializeField] private float _buildingHeight;
    private float _length;
    private float _width;
    private float _rotation;


    [SerializeField] private RectTransform _buildingRectTransform;
    
    [SerializeField] private GameObject _mainShadow;
    [SerializeField] private GameObject _shadow1;
    [SerializeField] private GameObject _shadow2;
    
    
    // DEBUGGING
    private Vector2[] shadowCornerGizmo;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBuildingShadows();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateBuildingShadows()
    {
        // Sun Vector
        Vector2 sunVector = Sun.direction * _buildingHeight * Sun.angleMultiplier;
        
        // Relevant building corners
        Vector2[] farsideCorners = GetFarsideBuildingCorners();
        
        // Shadow corners
        Vector2[] shadowCorners = GetOutsideShadowCorners(farsideCorners);
        shadowCornerGizmo = shadowCorners;
        
        // Shadow triangle rotation points
        Vector3 triangle1Pivot;
        Vector3 triangle2Pivot;
        
        // Scale and displace the main shadow
        _mainShadow.transform.localScale = _buildingRectTransform.localScale;
        Vector3 mainShadowPosition = _mainShadow.transform.position;
        mainShadowPosition.x += sunVector.x;
        mainShadowPosition.y += sunVector.y;
        _mainShadow.transform.position = mainShadowPosition;
        
        // Triangle sides on the actual building
        Vector2 triangle1Side = Vector3.Project(
            sunVector, farsideCorners[0] - farsideCorners[1]);
        Vector2 triangle2Side = Vector3.Project(
            sunVector, farsideCorners[0] - farsideCorners[2]);
        
        // Determine dimensions
        float triangle1ScaleX = triangle1Side.magnitude;
        float triangle1ScaleY = (sunVector - triangle1Side).magnitude;

        float triangle2ScaleX = triangle2Side.magnitude;
        float triangle2ScaleY = (sunVector - triangle2Side).magnitude;
        
        
        // Determine which side is the "righthand side" from the perspective of the sun direction
        if (Vector2.SignedAngle(sunVector, triangle1Side) > 0)
        {
            // switch scale
            (triangle1ScaleX, triangle1ScaleY) = (triangle1ScaleY, triangle1ScaleX);
            
            // rotate the vector that determines the triangle rotation by 90 degrees
            triangle1Side = new Vector2(-triangle1Side.y, triangle1Side.x);
            
            // Set world point
            triangle1Pivot = shadowCorners[1];
            triangle2Pivot = farsideCorners[2];
        }
        else
        {
            // other triangle has to be switched
            // switch scale
            (triangle2ScaleX, triangle2ScaleY) = (triangle2ScaleY, triangle2ScaleX);
            
            // rotate the vector that determines the triangle rotation by 90 degrees
            triangle2Side = new Vector2(-triangle2Side.y, triangle2Side.x);
            
            // Set world point
            triangle1Pivot = farsideCorners[1];
            triangle2Pivot = shadowCorners[2];
        }

        // Scale sprites
        _shadow1.transform.localScale = new Vector3(triangle1ScaleX, triangle1ScaleY, 1);
        _shadow2.transform.localScale = new Vector3(triangle2ScaleX, triangle2ScaleY, 1);
        
        // Triangle Rotation
        float triangle1Angle = Vector2.SignedAngle(Vector2.right, triangle1Side);
        float triangle2Angle = Vector2.SignedAngle(Vector2.right, triangle2Side);
        
        // Rotate shadow triangles
        _shadow1.transform.eulerAngles = new Vector3(0, 0, triangle1Angle);
        _shadow2.transform.eulerAngles = new Vector3(0, 0, triangle2Angle);
        
        // Place the shadow triangles
        _shadow1.transform.position = triangle1Pivot;
        _shadow2.transform.position = triangle2Pivot;
        
        /*
        // Shadow dimensions
        _shadow1.transform.localScale = new Vector3(
            Vector2.Distance(farsideCorners[0], shadowCorners[1]),
            Vector2.Distance(farsideCorners[1], shadowCorners[0]),
            1);
        _shadow2.transform.localScale = new Vector3(
            Vector2.Distance(farsideCorners[0], shadowCorners[2]),
            Vector2.Distance(farsideCorners[2], shadowCorners[0]),
            1);
        
        // Determine the shadow rotation
        float angle1 = Vector2.SignedAngle(Vector2.up, shadowCorners[1] - farsideCorners[0]);
        float angle2 = Vector2.SignedAngle(Vector2.up, shadowCorners[2] - farsideCorners[0]);
        
        // Rotate shadow piece
        _shadow1.transform.eulerAngles = new Vector3(0, 0, -angle1);
        _shadow2.transform.eulerAngles = new Vector3(0, 0, -angle2);
        
        // Place shadow piece at center point
        _shadow1.transform.position = (shadowCorners[1] + farsideCorners[0]) / 2;
        _shadow2.transform.position = (shadowCorners[2] + farsideCorners[0]) / 2;
        */
    }

    private Vector2[] GetFarsideBuildingCorners()
    {
        Vector3[] fourCorners = new Vector3[4];
        _buildingRectTransform.GetWorldCorners(fourCorners);

        // Calculate scalar projection into sun direction
        float[] distances = new float[4];
        for (int i = 0; i < 4; i++)
        {
            distances[i] = Vector2.Dot(fourCorners[i], Sun.direction);
        }
        
        // determine highest and lowest
        int high = 0;
        int low = 0;
        for (int i = 1; i < 4; i++)
        {
            if (distances[i] > distances[high])
            {
                high = i;
            }
            else if (distances[i] < distances[low])
            {
                low = i;
            }
        }

        Vector2[] result = new Vector2[3];
        result[0] = fourCorners[high];
        int j = 1;
        for (int i = 0; i < 4; i++)
        {
            if (i != high && i != low)
            {
                result[j] = fourCorners[i];
                j++;
            }
        }
        
        return result;
    }

    private Vector2[] GetOutsideShadowCorners(Vector2[] buildingCorners)
    {
        Vector2[] result = new Vector2[3];
        for (int i = 0; i < 3; i++)
        {
            result[i] = buildingCorners[i] + Sun.direction * (Sun.angleMultiplier * _buildingHeight);
        }

        return result;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < 3; i++)
        {
            //Gizmos.DrawSphere(shadowCornerGizmo[i], 0.01f);
        }
        
    }
}
