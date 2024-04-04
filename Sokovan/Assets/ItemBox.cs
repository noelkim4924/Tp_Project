using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Noel Kim 
/// A01259986
/// Manages the behavior and state of individual item boxes in the game.
/// This includes handling collisions with the endpoint to change the box color.
/// </summary>
public class ItemBox : MonoBehaviour
{
    /// <summary>
    /// Flag to track if the box is in an overlapped state with the endpoint.
    /// </summary>
    public bool isOveraped = false;

    /// <summary>
    /// Renderer component used to change the color of the box.
    /// </summary>
    private Renderer myRenderer;
    
    /// <summary>
    /// Color to change to when the box is touched or overlapped.
    /// </summary>
    public Color touchColor;

    /// <summary>
    /// The original color of the box to revert back to when it's not overlapped.
    /// </summary>
    private Color originalColor;
    
    /// <summary>
    /// Called before the first frame update to initialize the box state.
    /// </summary>
    void Start()
    {   
        myRenderer = GetComponent<Renderer>();
        originalColor = myRenderer.material.color;
    }

    // Update method is not used but included for potential future use.
    void Update()
    {
        
    }

    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    /// Changes the box's color if the colliding object is the endpoint.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            isOveraped = true;
            myRenderer.material.color = touchColor;
            Debug.Log("EndPoint arrived!");
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger collider.
    /// Resets the box's color and overlap status if the colliding object is the endpoint.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            isOveraped = false;
            myRenderer.material.color = originalColor;
        }
    }

    /// <summary>
    /// Called every frame a collider remains within the trigger.
    /// Ensures the box's color remains changed if the colliding object is still the endpoint.
    /// </summary>
    /// <param name="other">The collider within the trigger.</param>
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            isOveraped = true;
            myRenderer.material.color = touchColor;
        }
    }
}
