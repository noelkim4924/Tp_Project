using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the behavior and state of individual item boxes in the game.
public class ItemBox : MonoBehaviour
{
    // Flag to track if the box is in an overlapped state with the endpoint.
    public bool isOveraped = false;

    // Renderer component used to change the color of the box.
    private Renderer myRenderer;
    
    // Color to change to when the box is touched or overlapped.
    public Color touchColor;
    // The original color of the box to revert back to when it's not overlapped.
    private Color originalColor;
    
    // Start is called before the first frame update.
    void Start()
    {   
        // Get the Renderer component attached to this GameObject.
        myRenderer = GetComponent<Renderer>();
        // Store the original color of the material for later use.
        originalColor = myRenderer.material.color;
    }

    // Update is called once per frame, but it's not used here.
    void Update()
    {
        // Empty Update method since all behavior is driven by Unity's physics events.
    }

    // This method is called when another collider enters the trigger collider attached to this object.
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "EndPoint" tag.
        if(other.tag == "EndPoint")
        {
            // Set the flag to true since it's overlapped with the endpoint.
            isOveraped = true;
            // Change the material color to the touch color.
            myRenderer.material.color = touchColor;
        }
        // Log a message to the console to indicate the endpoint has been reached.
        Debug.Log("EndPoint arrived!");
    }

    // This method is called when another collider exits the trigger collider.
    void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has the "EndPoint" tag.
        if(other.tag == "EndPoint")
        {
            // Revert the flag to false as it's no longer overlapped with the endpoint.
            isOveraped = false;
            // Reset the material color to the original color.
            myRenderer.material.color = originalColor;
        }
    }

    // This method is similar to OnTriggerEnter but is called every frame the collider remains within the trigger.
    void OnTriggerStay(Collider other)
    {
        // Again, check if the colliding object has the "EndPoint" tag.
        if(other.tag == "EndPoint")
        {
            // Since the collider is still within the trigger, set the flag to true.
            isOveraped = true;
            // Ensure the material color remains the touch color.
            myRenderer.material.color = touchColor;
        }
    }
}
