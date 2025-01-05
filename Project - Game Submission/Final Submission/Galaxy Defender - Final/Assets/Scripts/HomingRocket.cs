using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    [Header("Parameters")]
    public float lockOnRange = 100f;  // Maximum distance to search for enemies
    public float homingStrength = 5f; // Strength of the rocket's turning to follow the target
    public float rocketSpeed = 15f;   // Speed at which the rocket moves towards the target

    [Space]

    [Header("Rocket Prefab and Spawn")]
    public GameObject homingRocketPrefab;  // The rocket prefab to instantiate
    public Transform homingRocketSpawn;    // The spawn point for the rocket

    private bool isLaunched = false; // Flag to check if the rocket is launched
    private Rigidbody rb; // Rigidbody to apply forces for movement
    private Transform player; // Reference to the player's position
    private Transform lockedOnTarget; // Internal locked target (for the rocket's behavior)
    private Collider rocketCollider; // The trigger collider of the rocket

    // Start is called before the first frame update
    void Start()
    {
        // Find and store the player's transform (assuming the player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the rocket's collider component
        rocketCollider = GetComponent<Collider>();
        rocketCollider.isTrigger = true; // Ensure the collider is set to trigger
    }

    // Update is called once per frame
    void Update()
    {
        // If the rocket is not launched, listen for key input to launch
        if (!isLaunched && Input.GetKeyDown(KeyCode.F)) // Press "F" to launch
        {
            LaunchRocket();
        }

        // If the rocket is launched, execute the homing behavior
        if (isLaunched)
        {
            FindClosestEnemy();  // Find and lock onto the nearest enemy
            HomeInOnTarget();    // Home in on the locked target
        }
    }

    // Launch the rocket from the player's position and initial velocity
    void LaunchRocket()
    {
        // Instantiate the rocket at the spawn point with the player's rotation
        GameObject rocket = Instantiate(homingRocketPrefab, homingRocketSpawn.position, homingRocketSpawn.rotation);

        // Get the Rigidbody component from the instantiated rocket
        Rigidbody rocketRb = rocket.GetComponent<Rigidbody>();
        rocketRb.isKinematic = false; // Allow the Rigidbody to be affected by physics

        // Set the initial velocity for the rocket
        rocketRb.velocity = player.forward * rocketSpeed;

        // Set the rocket to be launched
        HomingRocket rocketScript = rocket.GetComponent<HomingRocket>();
        rocketScript.isLaunched = true; // Mark the rocket as launched
    }

    // Find the closest enemy within the lockOnRange (you can call this directly when needed)
    void FindClosestEnemy()
    {
        if (lockedOnTarget != null) return;  // Only find a target if none is locked

        Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRange);
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;

        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy")) // Check if the collider is tagged as "Enemy"
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);

                // If this enemy is closer than the previous closest one, update the closest enemy
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        // If we found an enemy, lock onto it
        if (closestEnemy != null)
        {
            lockedOnTarget = closestEnemy;
            Debug.Log("Locked onto: " + lockedOnTarget.name); // For debugging purposes, show the locked target
        }
    }

    // Home in on the locked target
    void HomeInOnTarget()
    {
        if (lockedOnTarget != null) // If we have a target locked
        {
            // Calculate the direction to the target
            Vector3 direction = lockedOnTarget.position - transform.position;

            // Normalize the direction and apply the rocket speed
            Vector3 moveDirection = direction.normalized * rocketSpeed;

            // Move the rocket in the calculated direction
            transform.position += moveDirection * Time.deltaTime;

            // Rotate the rocket smoothly towards the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, homingStrength * Time.deltaTime);
        }
        else
        {
            // If no target is locked, the rocket moves straight ahead
            transform.position += transform.forward * rocketSpeed * Time.deltaTime;
        }
    }

    // Handle collision with the enemy or obstacles
    void OnTriggerEnter(Collider other)
    {
        // When the rocket's trigger collider touches an object
        if (other.CompareTag("Enemy")) // Check if we collided with an enemy
        {
            // Check if it's the closest enemy to lock onto
            if (lockedOnTarget == null || Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, lockedOnTarget.position))
            {
                // Lock onto the enemy
                lockedOnTarget = other.transform;
                Debug.Log("Locked onto new enemy: " + lockedOnTarget.name);
            }
        }
    }

    // Optional: Draw a sphere in the editor to visualize the lock-on range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lockOnRange); // Draw the sphere around the rocket
    }
}