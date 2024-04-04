using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOEL KIM
/// A01259986
/// Manages camera behavior to follow and focus on a target, with different states and zoom levels.
/// </summary>
public class CamFollow : MonoBehaviour
{
    /// <summary>
    /// Defines the possible states of the camera.
    /// </summary>
    public enum State
    {
        Idle,       // Camera is not following any target.
        Ready,      // Camera is ready to follow the target.
        Tracking    // Camera is actively tracking the target.
    }

    /// <summary>
    /// Sets the state of the camera and adjusts the zoom level accordingly.
    /// </summary>
    private State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyShotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    /// <summary>
    /// The target for the camera to follow.
    /// </summary>
    private Transform target;

    /// <summary>
    /// Smooth time for camera movement and zoom.
    /// </summary>
    public float smoothTime = 0.2f;

    private Vector3 movingVelocity;
    private Vector3 targetPosition;

    /// <summary>
    /// Reference to the Camera component.
    /// </summary>
    private Camera cam;

    /// <summary>
    /// The zoom size the camera is trying to reach.
    /// </summary>
    private float targetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;

    private float lastZoomSpeed;

    /// <summary>
    /// Initializes the camera and sets its initial state to Idle.
    /// </summary>
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    /// <summary>
    /// Moves the camera smoothly towards the target's position.
    /// </summary>
    private void Move()
    {
        targetPosition = target.transform.position;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref movingVelocity, smoothTime);
        transform.position = targetPosition;
    }

    /// <summary>
    /// Smoothly adjusts the camera's zoom level.
    /// </summary>
    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    /// <summary>
    /// Updates the camera position and zoom based on the target's position, if assigned.
    /// </summary>
    private void FixedUpdate()
    {
        if (target != null)
        {
            Move();
            Zoom();
        }
    }

    /// <summary>
    /// Resets the camera's state to Idle.
    /// </summary>
    public void Reset()
    {
        state = State.Idle;
    }

    /// <summary>
    /// Sets the camera's target and updates its state.
    /// </summary>
    /// <param name="newTarget">The new target for the camera to follow.</param>
    /// <param name="newState">The new state of the camera.</param>
    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
