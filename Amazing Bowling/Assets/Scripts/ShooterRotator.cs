using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The ShooterRotator class is responsible for controlling the orientation of the ball shooter.
public class ShooterRotator : MonoBehaviour
{
    // Enumeration defining the potential states of rotation.
    private enum RotateState
    {
        Idle, // Not rotating.
        Vertical, // Rotating vertically.
        Horizontal, // Rotating horizontally.
        Ready // Positioned and ready to fire.
    }

    // Current state of the shooter, starting as Idle.
    private RotateState state = RotateState.Idle;

    // Rotation speed for both vertical and horizontal movements, adjustable via the Unity Inspector.
    public float verticalRotateSpeed = 360f;
    public float horizontalRotateSpeed = 360f;

    // Reference to the BallShooter script, which controls the shooting mechanism.
    public BallShooter ballShooter;

    // Update is called once per frame.
    void Update()
    {
        // State machine controlling the rotation of the shooter based on user input.
        switch(state)
        {
            case RotateState.Idle:
                // If the Fire1 button is pressed, transition to the Horizontal rotation state.
                if(Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
            break;

            case RotateState.Horizontal:
                // While the Fire1 button is held down, rotate horizontally.
                if(Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                }
                // Transition to Vertical rotation state upon release.
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
            break;

            case RotateState.Vertical:
                // While the Fire1 button is held down, rotate vertically.
                if(Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                // Transition to Ready state upon release and enable the BallShooter script.
                else if(Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true; // Allow shooting once the rotation is set.
                }
            break;

            case RotateState.Ready:
                // In the Ready state, no rotation occurs. Awaiting shooting command.
            break;
        }
    }

    // Called when the script becomes active in the game.
    private void OnEnable()
    {
        // Reset rotation and state.
        transform.rotation = Quaternion.identity; // Reset rotation to no rotation.
        state = RotateState.Idle; // Set the initial state to Idle.
        ballShooter.enabled = false; // Disable shooting until properly aimed.
    }
}
