using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Noel Kim
/// A01259986
/// Continuously rotates an object around its x, y, and z axes, creating a spinning effect.
/// </summary>
public class Rotator : MonoBehaviour
{
    /// <summary>
    /// Rotates the object around its axes each frame, ensuring smooth, time-based rotation.
    /// </summary>
    void Update()
    {
        transform.Rotate(60 * Time.deltaTime,  // Rotation around the x-axis
                         60 * Time.deltaTime,  // Rotation around the y-axis
                         60 * Time.deltaTime); // Rotation around the z-axis
    }
}
