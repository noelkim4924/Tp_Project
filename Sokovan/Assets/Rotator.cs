using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for continuously rotating an object.
public class Rotator : MonoBehaviour
{
    // Update is called once per frame. The actual frequency of calls can vary depending on frame rate.
    void Update()
    {
        // Rotate the object around its x, y, and z axes every frame.
        // Time.deltaTime is the time in seconds it took to complete the last frame,
        // ensuring smooth rotation regardless of frame rate.
        // Here, the object will rotate 60 degrees per second around each axis.
        transform.Rotate(60 * Time.deltaTime,  // Rotation around the x-axis
                         60 * Time.deltaTime,  // Rotation around the y-axis
                         60 * Time.deltaTime); // Rotation around the z-axis
    }
}
