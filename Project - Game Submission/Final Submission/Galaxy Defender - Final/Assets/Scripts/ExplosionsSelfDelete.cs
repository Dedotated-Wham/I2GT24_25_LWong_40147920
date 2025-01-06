using UnityEngine;
using System.Collections;

public class ExplosionSelfDelete : MonoBehaviour
{
    private ParticleSystem explosionParticles; // Reference to the particle system

    private void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        explosionParticles = GetComponent<ParticleSystem>();

        // If there's a particle system, start a coroutine to delete after the duration
        if (explosionParticles != null)
        {
            // Wait for the duration of the particle system and then destroy the game object
            StartCoroutine(DestroyExplosionAfterDuration());
        }
        else
        {
            // If no particle system, destroy the object immediately (or after a small delay)
            Destroy(gameObject);
        }
    }

    // Coroutine to destroy the game object after the duration of the particle system
    private IEnumerator DestroyExplosionAfterDuration()
    {
        // Wait for the particle system's duration to finish
        yield return new WaitForSeconds(4f);

        // Destroy the game object once the particles are done
        Destroy(gameObject);
    }
}
