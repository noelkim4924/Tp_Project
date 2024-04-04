using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for controlling the player's movement.
public class Player : MonoBehaviour
{
    // Reference to the GameManager to check the game state.
    public GameManager gameManager;

    // Movement speed of the player.
    public float speed = 10f;

    // The Rigidbody component attached to the player for physics-based movement.
    private Rigidbody playerRigidbody; 

    // Start is called before the first frame update.
    void Start()
    {
        // Get the Rigidbody component from the current GameObject.
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame.
    void Update()
    {
        // If the game is over, disable player movement.
        if(gameManager.isGameOver == true)
        {
            return;
        }

        // Get user input for horizontal and vertical movement.
        // "Horizontal" and "Vertical" are built-in Unity inputs configured in the Input Manager.
        // They represent the arrow keys (or WASD keys) and return a value between -1 and 1.
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Keep the player's current vertical velocity to maintain gravity's effect.
        float fallSpeed = playerRigidbody.velocity.y;

        // Create a new velocity vector based on input, speed, and maintaining current fall speed.
        Vector3 velocity = new Vector3(inputX, 0, inputZ) * speed;
        velocity.y = fallSpeed; // Apply the original fall speed to the y-axis velocity.

        // Assign the new velocity to the Rigidbody to move the player.
        playerRigidbody.velocity = velocity;
    }
}
