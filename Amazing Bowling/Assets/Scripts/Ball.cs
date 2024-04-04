using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOEL KIM
/// A01259986
/// Handles the behavior of a ball that explodes on impact, dealing damage to nearby props.
/// </summary>
public class Ball : MonoBehaviour
{
    /// <summary>
    /// Layer to detect collisions with props only.
    /// </summary>
    public LayerMask whatIsProp;

    /// <summary>
    /// Particle system for the explosion effect.
    /// </summary>
    public ParticleSystem explosionParticle;

    /// <summary>
    /// Audio source for playing explosion sound.
    /// </summary>
    public AudioSource explosionAudio;

    /// <summary>
    /// Maximum damage the ball can cause at the center of the explosion.
    /// </summary>
    public float maxDamage = 100f;

    /// <summary>
    /// Force of the explosion effect applied to nearby objects.
    /// </summary>
    public float explosionForce = 1000f;

    /// <summary>
    /// Time in seconds before the ball is automatically destroyed.
    /// </summary>
    public float lifeTime = 10f;

    /// <summary>
    /// Radius of the explosion effect.
    /// </summary>
    public float explosionRadius = 20f;

    /// <summary>
    /// Destroys the ball object after its lifetime has expired.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// Notifies the GameManager when the ball is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        GameManager.instance.OnBallDestroy();
    }

    /// <summary>
    /// Triggers an explosion when the ball collides with an object.
    /// </summary>
    /// <param name="other">The Collider that the ball collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);

        foreach (Collider hit in colliders)
        {
            Rigidbody targetRigidbody = hit.GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop targetProp = hit.GetComponent<Prop>();
            float damage = CalculateDamage(hit.transform.position);
            targetProp.TakeDamage(damage);
        }

        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
        Destroy(gameObject);
    }

    /// <summary>
    /// Calculates the damage inflicted to a target based on its distance from the explosion center.
    /// </summary>
    /// <param name="targetPosition">The position of the target being damaged.</param>
    /// <returns>The calculated damage.</returns>
    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float distance = explosionToTarget.magnitude;
        float edgeToCenterDistance = explosionRadius - distance;
        float percentage = edgeToCenterDistance / explosionRadius;
        float damage = maxDamage * percentage;
        return Mathf.Max(0, damage);
    }
}
