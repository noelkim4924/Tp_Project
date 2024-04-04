using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the camera behavior to follow and zoom on the target.
public class CamFollow : MonoBehaviour
{
    // Enum to define the different states of the camera.
    public enum State
    {
        Idle,       // Camera is not following any target, it's idle.
        Ready,      // Camera is ready to follow the target.
        Tracking    // Camera is actively tracking the target.
    }

    // Property to set the state of the camera and adjust the zoom accordingly.
    private State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    // Set camera zoom for when the camera is idle.
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    // Set camera zoom for when the camera is ready to track.
                    targetZoomSize = readyShotZoomSize;
                    break;
                case State.Tracking:
                    // Set camera zoom for when the camera is tracking the target.
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    // The target for the camera to follow.
    private Transform target;

    public float smoothTime = 0.2f; // Smooth time for camera movement and zoom.

    // Velocity reference for smooth damping.
    private Vector3 movingVelocity;
    // Calculated target position for the camera.
    private Vector3 targetPosition;

    // Reference to the Camera component.
    private Camera cam;
    // The zoom size the camera is trying to reach.
    private float targetZoomSize = 5f;

    // Constants to define the camera's zoom levels for different states.
    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;

    // Last zoom speed reference for smooth damping.
    private float lastZoomSpeed;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Get the Camera component from the child of the current GameObject.
        cam = GetComponentInChildren<Camera>();
        // Set the initial state to Idle.
        state = State.Idle;
    }

    // Move the camera smoothly towards the target position.
    private void Move()
    {
        // Get the current target's position.
        targetPosition = target.transform.position;

        // Calculate a smooth position between the current position and the target's position.
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref movingVelocity, smoothTime);

        // Apply the calculated position to the camera's transform.
        transform.position = targetPosition;
    }

    // Smoothly adjust the camera's zoom level.
    private void Zoom()
    {
        // Calculate a smooth zoom size between the current size and the target size.
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);

        // Apply the calculated zoom size to the camera's orthographic size.
        cam.orthographicSize = smoothZoomSize;
    }

    // FixedUpdate is called every fixed framerate frame.
    private void FixedUpdate()
    {
        // If a target is assigned, move and zoom the camera based on the target's position.
        if (target != null)
        {
            Move();
            Zoom();
        }
    }

    // Resets the camera's state to Idle.
    public void Reset()
    {
        state = State.Idle;
    }

    // Sets the camera's target and updates its state.
    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
