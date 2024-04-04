using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOEL KIM
/// A01259986
/// Manages properties and behaviors of destructible objects in the game, 
/// including their health, the score they provide upon destruction, 
/// and the visual and audio effects that play when they are destroyed.
/// </summary>
public class Prop : MonoBehaviour
{
    public int score = 5; // The score value this prop will provide upon destruction.
    public ParticleSystem explosionParticle; // The particle system that plays when the prop is destroyed.
    public float hp = 10f; // Health points of the prop, determining how much damage it can take before being destroyed.

    /// <summary>
    /// Applies damage to the prop, potentially destroying it if health drops to zero or below.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the prop.</param>
    public void TakeDamage(float damage)
    {
        hp -= damage; // Subtract the damage amount from the prop's health.

        if (hp <= 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);
            instance.Play();
            
            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            GameManager.instance.AddScore(score);
            
            Destroy(instance.gameObject, instance.main.duration);
            gameObject.SetActive(false);
        }
    }
}
