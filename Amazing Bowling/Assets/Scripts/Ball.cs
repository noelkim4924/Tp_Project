using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Public variables can be set from the Unity Editor
    public LayerMask whatIsProp; // Layer to detect collisions with props only
    public ParticleSystem explosionParticle; // Particle system for the explosion effect
    public AudioSource explosionAudio; // Audio source for playing explosion sound

    public float maxDamage = 100f; // Maximum damage the ball can cause at the center of the explosion
    public float explosionForce = 1000f; // Force of the explosion effect applied to nearby objects

    public float lifeTime = 10f; // Time in seconds before the ball is automatically destroyed
    public float explosionRadius = 20f; // Radius of the explosion effect

    // Start is called before the first frame update
    void Start()
    {
        // Automatically destroy the ball object after its lifetime has expired
        Destroy(gameObject, lifeTime);
    }
    private void OnDestroy()
{
 GameManager.instance.OnBallDestroy();
}

    // OnTriggerEnter is called when the Collider 'other' enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Create an array of all colliders within the explosion radius that match the layer mask 'whatIsProp'
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);

        // Iterate through all the colliders
        for (int i = 0; i < colliders.Length; i++)
        {
            // Get the Rigidbody component from the collider object
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // Apply an explosion force to the Rigidbody
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            // Get the Prop component from the collider object
            Prop targetProp = colliders[i].GetComponent<Prop>();

            // Calculate damage based on the distance from the explosion
            float damage = CalculateDamage(colliders[i].transform.position);

            // Apply damage to the Prop
            targetProp.TakeDamage(damage);
        }

        // Detach the particle system from the parent
        explosionParticle.transform.parent = null;
        
        // Play the explosion particle effect and sound
        explosionParticle.Play();
        explosionAudio.Play();

        // Notify the GameManager that the ball is destroyed
        
        // Duplicate line, possible mistake - consider removing
        // GameManager.instance.OnBallDestroy();

        // Destroy the particle system after the effect has completed
        Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
        // Destroy the ball object
        Destroy(gameObject);
    }

    // Calculate the damage based on the target's position relative to the explosion center
    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the vector from the explosion to the target
        Vector3 explosionToTarget = targetPosition - transform.position;
        // Get the distance from the explosion to the target
        float distance = explosionToTarget.magnitude;
        
        // Calculate the distance from the edge of the explosion radius to the target's center
        float edgeToCenterDistance = explosionRadius - distance;
        
        // Calculate the damage percentage relative to the explosion center
        float percentage = edgeToCenterDistance / explosionRadius;
        
        // Calculate the actual damage based on the maximum damage and the calculated percentage
        float damage = maxDamage * percentage;

        // Ensure that the damage is not negative
        damage = Mathf.Max(0, damage);
        return damage;
    }
}
