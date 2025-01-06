using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    
    private GameManager gameManager;         //Reference GameManager script and call it gameManager inside this script.
    private int enemyScoreToAdd = 2;
    private int obstacleScoreToAdd = 1;
    private HomingRocket homingRocketScript; // Reference to the HomingRocket script
    private AudioSource projectileAudio;

    private Transform target; // The target the rocket will home in on
    private bool homingActive = false; // Flag to check if homing is active
    private float homingStrength;
    private float speed;

    

    public GameObject explosionPrefabEnemy;
    public GameObject explosionPrefabObstacle;

    [Header("Sound Effects")]
    public AudioClip obstacleSound;
    public AudioClip enemySound;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        homingRocketScript = playerObject.GetComponentInParent<HomingRocket>();

        if (homingRocketScript == null)
        {
            Debug.LogError("HomingRocket script not found on player!");
        }

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();         //Find Game Manager object and find it's game manager script.
        projectileAudio = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if (homingActive && target != null)
        {
            // Home in on the target
            HomeInOnTarget();
        }
        else
        {
            // If no target, just move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // Start the homing behavior
    public void StartHoming(float lockOnRange, float homingStrength, float speed)
    {
        this.homingStrength = homingStrength;
        this.speed = speed;

        // Find the closest enemy to lock onto
        FindClosestEnemy(lockOnRange);
    }

    // Find the closest enemy within the given range
    void FindClosestEnemy(float lockOnRange)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRange);
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;

        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        // Lock onto the closest enemy if one is found
        if (closestEnemy != null)
        {
            target = closestEnemy;
            homingActive = true; // Start homing after locking onto the target
            Debug.Log("Homing rocket locked onto: " + target.name);
        }
    }

    // Move the rocket towards the target
    void HomeInOnTarget()
    {
        // Calculate direction to target and move the rocket
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, homingStrength * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Handle collisions with enemies, obstacles, and the environment
    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            Debug.LogError("Collider is null in OnTriggerEnter!");
            return;
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (projectileAudio != null && enemySound != null)
            {
                projectileAudio.PlayOneShot(enemySound, 0.4f); // Play the sound with a delay if necessary
            }

            // Instantiate explosion on enemy object position
            Instantiate(explosionPrefabEnemy, transform.position, transform.rotation);

            // Destroy the enemy and the rocket
            Destroy(other.gameObject);
            StartCoroutine(DestroyAfterSound()); // Adjust the delay to the length of the sound if needed

            gameManager.UpdateScore(enemyScoreToAdd);    //Add score to the game manager script whenever an enemy is destroyed.

            // Reset the launch flag in the HomingRocket script
            homingRocketScript.ResetLaunchFlag();
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            if (projectileAudio != null && obstacleSound != null)
            {
                projectileAudio.PlayOneShot(obstacleSound, 0.4f); // Play the sound with a delay if necessary
            }
            // Instantiate explosion for obstacles
            Instantiate(explosionPrefabObstacle, transform.position, transform.rotation);

            // Destroy the rocket and the obstacle
            Destroy(other.gameObject);
            StartCoroutine(DestroyAfterSound()); // Adjust the delay to the length of the sound if needed

            gameManager.UpdateScore(obstacleScoreToAdd);    //Add score to the game manager script whenever an enemy is destroyed.
            // Reset the launch flag in the HomingRocket script
            homingRocketScript.ResetLaunchFlag();
        }
        else if (other.gameObject.tag == "Environment")
        {
            // Destroy the rocket when it hits the environment
            Destroy(gameObject);

            // Reset the launch flag in the HomingRocket script
            homingRocketScript.ResetLaunchFlag();
        }
    }
    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(0.1f); // Wait for the specified amount of time (enough for the sound to finish)
        Destroy(gameObject); // Now destroy the projectile after the delay
    }
}
