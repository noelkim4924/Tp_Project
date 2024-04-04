using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is responsible for controlling the ball shooting mechanics.
public class BallShooter : MonoBehaviour
{
    // Public variables to be set in the Unity Editor.
    public CamFollow cam; // Camera follow script to adjust the camera view.
    public Rigidbody ball; // The rigidbody of the ball prefab to be shot.

    public Transform firePos; // The position from which the ball will be fired.
    public Slider powerSlider; // UI element to visually indicate the current power level.

    public AudioSource shootingAudio; // The audio source for playing shooting sounds.
    public AudioClip fireClip; // Audio clip to play when firing the ball.
    public AudioClip chargingClip; // Audio clip to play when charging the shot.

    public float minForce = 15f; // Minimum force applied when shooting the ball.
    public float maxForce = 30f; // Maximum force that can be applied.
    public float chargingTime = 0.75f; // Time it takes to charge from min to max force.

    // Private variables used internally by the script.
    private float currentForce; // The current force to be applied to the next shot.
    private float chargeSpeed; // The rate at which the shot power increases.
    private bool fired; // Flag to determine if the ball has been shot.

    // Called when the script is enabled.
    private void OnEnable()
    {
        // Initialize force variables and UI.
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false; // Ensure the ball is not marked as fired when the game starts.
    }

    // Start is called before the first frame update.
    private void Start()
    {
        // Calculate the speed at which the ball charge increases.
        chargeSpeed = (maxForce - minForce) / chargingTime;
    }

    // Update is called once per frame.
    private void Update()
    {
        // If the ball has been fired, exit this update loop to prevent further code execution.
        if (fired)
        {
            return;
        }

        // Reset the power slider's value to the minimum force at the start of each frame.
        powerSlider.value = minForce;

        // Check if the fire button has been released and if the current force is at its max.
        if (currentForce >= maxForce && !fired)
        {
            // If so, set the current force to the max and fire the ball.
            currentForce = maxForce;
            Fire();
        }
        else if (Input.GetButtonDown("Fire1")) // If the fire button is initially pressed...
        {
            // ...reset the fired flag and current force, and play the charging audio.
            fired = false;
            currentForce = minForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if (Input.GetButton("Fire1") && !fired) // While the fire button is held down...
        {
            // ...increase the current force based on the charge speed and time,
            // updating the power slider's value to reflect this.
            currentForce += chargeSpeed * Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if (Input.GetButtonUp("Fire1") && !fired) // When the fire button is released...
        {
            // ...fire the ball.
            Fire();
        }
    }

    // Function to handle the actual firing of the ball.
    private void Fire()
    {
        // Mark the ball as fired to prevent re-firing until the next shot is ready.
        fired = true;

        // Instantiate a new ball instance at the fire position with the correct rotation.
        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);

        // Set the velocity of the ball instance to the current force multiplied by the forward direction.
        ballInstance.velocity = currentForce * firePos.forward;

        // Play the firing audio clip.
        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        // Reset the current force back to the minimum after firing.
        currentForce = minForce;
        //powerSlider.value = minForce; // Uncomment this line if you want to reset the slider UI.

        // Tell the camera to follow the newly fired ball.
        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
