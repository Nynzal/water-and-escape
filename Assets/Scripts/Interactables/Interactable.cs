using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Class for all objects, buildings, things that can be interacted with
    // Interaction is based on a timer, how long the thing should be interacted with to trigger its effect
    // Has to be connected to another class for the actual object
    
    // Interaction radius
    public Collider2D _interactionCollider;
    
    // World Anchor for the interaction popup UI element
    public Vector2 _uiOffset;
    
    // Handle the pickup of the interactable
    public abstract void CompleteInteraction();

    
    // Interaction start
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            EventManager.Instance.OnEnteringInteractionArea(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.OnLeavingInteractionArea(this);
        }
    }

    public Vector2 GetPromptWorldAnchor()
    {
        return new Vector2(transform.position.x, transform.position.y) + _uiOffset;
    }

    // Interactable consumed / collected
    protected void CleanUp()
    {
        Destroy(gameObject);
    }
}
