using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NOEL KIM
/// A01259986
/// Controls the ball shooting mechanics, including charging and firing the ball with variable force.
/// </summary>
public class BallShooter : MonoBehaviour
{
    /// <summary>
    /// Camera follow script to adjust the camera view.
    /// </summary>
    public CamFollow cam;

    /// <summary>
    /// The rigidbody of the ball prefab to be shot.
    /// </summary>
    public Rigidbody ball;

    /// <summary>
    /// The position from which the ball will be fired.
    /// </summary>
    public Transform firePos;

    /// <summary>
    /// UI element to visually indicate the current power level.
    /// </summary>
    public Slider powerSlider;

    /// <summary>
    /// The audio source for playing shooting sounds.
    /// </summary>
    public AudioSource shootingAudio;

    /// <summary>
    /// Audio clip to play when firing the ball.
    /// </summary>
    public AudioClip fireClip;

    /// <summary>
    /// Audio clip to play when charging the shot.
    /// </summary>
    public AudioClip chargingClip;

    /// <summary>
    /// Minimum force applied when shooting the ball.
    /// </summary>
    public float minForce = 15f;

    /// <summary>
    /// Maximum force that can be applied.
    /// </summary>
    public float maxForce = 30f;

    /// <summary>
    /// Time it takes to charge from min to max force.
    /// </summary>
    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool fired;

    /// <summary>
    /// Initializes the ball shooter by setting up force variables and UI elements.
    /// </summary>
    private void OnEnable()
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    /// <summary>
    /// Calculates the charge speed based on the charging time.
    /// </summary>
    private void Start()
    {
        chargeSpeed = (maxForce - minForce) / chargingTime;
    }

    /// <summary>
    /// Handles user input to charge and fire the ball.
    /// </summary>
    private void Update()
    {
        if (fired) return;

        powerSlider.value = minForce;

        if (currentForce >= maxForce && !fired)
        {
            currentForce = maxForce;
            Fire();
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            fired = false;
            currentForce = minForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if (Input.GetButton("Fire1") && !fired)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if (Input.GetButtonUp("Fire1") && !fired)
        {
            Fire();
        }
    }

    /// <summary>
    /// Fires a ball with the charged force and resets the shooting mechanics.
    /// </summary>
    private void Fire()
    {
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);
        ballInstance.velocity = currentForce * firePos.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
