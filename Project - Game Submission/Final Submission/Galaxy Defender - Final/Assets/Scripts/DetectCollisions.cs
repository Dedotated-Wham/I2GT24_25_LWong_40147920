using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;         //Reference GameManager script and call it gameManager inside this script.
    private int enemyScoreToAdd = 2;
    private int obstacleScoreToAdd = 1;
    private AudioSource projectileAudio;

    private HomingRocket homingRocketScript; // Reference to the HomingRocket script

    public GameObject explosionPrefabEnemy;
    public GameObject explosionPrefabObstacle;

    [Header("Sound Effects")]

    public AudioClip obstacleSound;
    public AudioClip enemySound;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();         //Find Game Manager object and find it's game manager script.
        projectileAudio = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            if (projectileAudio != null && enemySound != null)
            {
                projectileAudio.PlayOneShot(enemySound, 0.4f); // Play the sound with a delay if necessary
            }

            Instantiate(explosionPrefabEnemy, transform.position, transform.rotation);  // Instantiate explosion on enemy object position.
            Destroy(other.gameObject);              // This will destroy the enemy object.
            //Destroy(gameObject);
            // Call a coroutine to delay destruction of the projectile to let the sound play
            StartCoroutine(DestroyAfterSound()); // Adjust the delay to the length of the sound if needed

            gameManager.UpdateScore(enemyScoreToAdd);    // Add score to the game manager script whenever an enemy is destroyed.
        }

        if (other.gameObject.tag == "Obstacle")

        {
            if (obstacleSound != null && projectileAudio != null)
            {
                projectileAudio.PlayOneShot(obstacleSound, 0.4f); // Play the sound for obstacle collision
            }

            Instantiate(explosionPrefabObstacle, transform.position, transform.rotation);  // Instantiate explosion on obstacle object position.
            Destroy(other.gameObject);          // This will destroy the obstacle.
            //Destroy(gameObject);
            // Call a coroutine to delay destruction of the projectile
            StartCoroutine(DestroyAfterSound()); // Adjust the delay to the length of the sound if needed

            gameManager.UpdateScore(obstacleScoreToAdd);    // Add score to the game manager script whenever an enemy is destroyed.
        }

        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);                //This will destroy the player projectile when it hits the environment.
            //Debug.Log("Bullet Hit Environment");
        }
    }
    
    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(0.1f); // Wait for the specified amount of time (enough for the sound to finish)
        Destroy(gameObject); // Now destroy the projectile after the delay
    }
    
}
