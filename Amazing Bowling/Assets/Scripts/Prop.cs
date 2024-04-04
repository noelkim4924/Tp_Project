using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the properties and behaviors of destructible objects (props) in the game.
public class Prop : MonoBehaviour
{
    public int score = 5; // The score value this prop will provide upon destruction.

    public ParticleSystem explosionParticle; // The particle system that plays when the prop is destroyed.
    public float hp = 10f; // Health points of the prop, which determine how much damage it can take before being destroyed.

    // This method is called to apply damage to the prop.
    public void TakeDamage(float damage)
    {
        hp -= damage; // Subtract the damage amount from the prop's health.

        // Check if the prop's health is less than or equal to zero, meaning it should be destroyed.
        if (hp <= 0)
        {
            // Instantiate the explosion particle effect at the prop's position and rotation.
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);
            instance.Play(); // Play the explosion effect.

            // Try to get an AudioSource component attached to the explosion particle system instance.
            AudioSource explosionAudio = instance.GetComponent<AudioSource>();

            // Add the score value of this prop to the game manager's total score.
            GameManager.instance.AddScore(score);

            // Duplicate line - possibly a mistake or for doubling the score. Normally you would only call AddScore once.
            // GameManager.instance.AddScore(score);
         
            // Destroy the particle effect instance after its duration has elapsed.
            Destroy(instance.gameObject, instance.main.duration);

            // Deactivate the prop game object. This could be a part of an object pooling system, which is a more efficient way to handle objects that are frequently created and destroyed.
            gameObject.SetActive(false);
        }
    }
}
