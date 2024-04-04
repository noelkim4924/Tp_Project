using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Noel kim
/// A01259986
/// Controls the player's movement within the game, utilizing Unity's physics system for movement.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Reference to the GameManager to check if the game is over.
    /// </summary>
    public GameManager gameManager;

    /// <summary>
    /// The speed at which the player moves.
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// The Rigidbody component attached to the player for physics-based movement.
    /// </summary>
    private Rigidbody playerRigidbody;

    /// <summary>
    /// Initializes the player's movement system by retrieving the Rigidbody component.
    /// </summary>
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Updates the player's position each frame based on user input, provided the game is not over.
    /// </summary>
    void Update()
    {
        if (gameManager.isGameOver == true)
        {
            return;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        float fallSpeed = playerRigidbody.velocity.y;

        Vector3 velocity = new Vector3(inputX, 0, inputZ) * speed;
        velocity.y = fallSpeed;

        playerRigidbody.velocity = velocity;
    }
}
