using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : Interactable
{
    [SerializeField] private float _waterAvailable;

    [SerializeField] private float _evaporationFactor;

    [SerializeField] private int _heatBaseline;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CompleteInteraction()
    {
        EventManager.Instance.OnAccessingWater(this);
    }

    public float TakeWater(float maximum)
    {
        float waterReturn;
        if (maximum > _waterAvailable)
        {
            waterReturn = _waterAvailable;
            _waterAvailable = 0;
            DryOutWaterSource();
        }
        else
        {
            waterReturn = maximum;
            _waterAvailable -= maximum;
        }

        return waterReturn;
    }

    private void DryOutWaterSource()
    {
        // handle the drying out of water source here
        _interactionCollider.enabled = false;
        CleanUp();
    }
}
