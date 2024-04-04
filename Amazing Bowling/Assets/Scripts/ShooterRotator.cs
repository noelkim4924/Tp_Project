using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOEL KIM
/// A01259986
/// Controls the orientation of the ball shooter, allowing it to rotate horizontally and vertically 
/// based on user input before allowing a ball to be shot.
/// </summary>
public class ShooterRotator : MonoBehaviour
{
    /// <summary>
    /// Defines the possible rotation states of the ball shooter.
    /// </summary>
    private enum RotateState
    {
        /// <summary>
        /// The shooter is not rotating.
        /// </summary>
        Idle,

        /// <summary>
        /// The shooter is rotating vertically.
        /// </summary>
        Vertical,

        /// <summary>
        /// The shooter is rotating horizontally.
        /// </summary>
        Horizontal,

        /// <summary>
        /// The shooter is positioned and ready to fire.
        /// </summary>
        Ready
    }

    /// <summary>
    /// The current rotation state of the shooter, initialized to Idle.
    /// </summary>
    private RotateState state = RotateState.Idle;

    /// <summary>
    /// The rotation speed for vertical movements, adjustable via the Unity Inspector.
    /// </summary>
    public float verticalRotateSpeed = 360f;

    /// <summary>
    /// The rotation speed for horizontal movements, adjustable via the Unity Inspector.
    /// </summary>
    public float horizontalRotateSpeed = 360f;

    /// <summary>
    /// Reference to the BallShooter script, which controls the shooting mechanism.
    /// </summary>
    public BallShooter ballShooter;

    /// <summary>
    /// Updates the rotation state of the shooter based on user input each frame.
    /// </summary>
    void Update()
    {
        switch(state)
        {
            case RotateState.Idle:
                if(Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
                break;

            case RotateState.Horizontal:
                if(Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
                break;

            case RotateState.Vertical:
                if(Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if(Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true;
                }
                break;

            case RotateState.Ready:
                // Ready state does not require specific actions each frame.
                break;
        }
    }

    /// <summary>
    /// Resets the shooter's rotation and state when the script becomes active.
    /// </summary>
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        state = RotateState.Idle;
        ballShooter.enabled = false;
    }
}
