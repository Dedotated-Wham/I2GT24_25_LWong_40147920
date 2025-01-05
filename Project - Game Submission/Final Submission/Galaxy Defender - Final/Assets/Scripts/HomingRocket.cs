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
    private Transform player; // Reference to the player's position
    private Transform lockedOnTarget; // Internal locked target (for the rocket's behavior)
    private AltProjectileMoveForward rocketMoveScript; // Reference to the projectile movement script

    // Start is called before the first frame update
    void Start()
    {
        // Find and store the player's transform (assuming the player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If the rocket is not launched, listen for key input to launch
        if (!isLaunched && Input.GetKeyDown(KeyCode.F)) // Press "F" to launch
        {
            LaunchRocket();
        }
    }

    // Launch the rocket from the player's position and initial velocity
    void LaunchRocket()
    {
        // Instantiate the rocket at the spawn point with the player's rotation
        GameObject rocket = Instantiate(homingRocketPrefab, homingRocketSpawn.position, homingRocketSpawn.rotation);

        // Get the AltProjectileMoveForward component from the instantiated rocket
        rocketMoveScript = rocket.GetComponent<AltProjectileMoveForward>();

        // Mark the rocket as launched by activating homing (for example, if your rocket has a movement script)
        HomingProjectile rocketScript = rocket.GetComponent<HomingProjectile>();
        rocketScript.StartHoming(lockOnRange, homingStrength, rocketSpeed);

        // Set the flag to indicate the rocket has been launched
        isLaunched = true;

        // Reset flag once the projectile is destroyed
        Destroy(rocket, 5f); // Set lifetime (same as in AltProjectileMoveForward script)
    }

    // You can use this method to reset the 'isLaunched' flag from the HomingRocket script
    public void ResetLaunchFlag()
    {
        Debug.Log("Rocket launch flag reset.");
        isLaunched = false;
    }
}
