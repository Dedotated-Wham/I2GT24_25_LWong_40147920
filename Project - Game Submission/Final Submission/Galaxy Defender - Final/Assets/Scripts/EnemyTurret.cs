using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform player;                //Reference to the player's transform position.
    public Transform turretHead;            //Reference to the gun/barrel turretHead (child object with gun barrel to rotate).
    public GameObject projectilePrefab;     //Enemy projectile to use.
    public Transform leftFirePoint;         //Fire point for the left gun barrel. Part of the gun object.
    public Transform rightFirePoint;        //Fire point for the right gun barrel. Part of the gun object.
    public float fireRate = 1.0f;           //Time between shots.
    public float rotationSpeed = 10.0f;      //Speed of the rotation for the Turret Head.
    public float detectionRange = 100f;    //The range at which the turret detects the player.
    public float projectileSpeed = 50f;  //Projectile Speed
    public float predictionLeadTime = 5.0f; // How much ahead of the player the turret should fire (in seconds).


    private float nextFireTime = 0f;        //Time when turret is allowed to fire again.
    private bool isPlayerInRange = false;   //Check whether player is in range of the turret's detection.



    // Update is called once per frame
    void Update()
    {
        CheckPlayerInRange();

        if (isPlayerInRange)
        {
            RotateTurretTowardsPlayer();
            //Debug.Log("Player Is In Range");

            if (Time.time >= nextFireTime)
            {
                FireProjectiles();
                nextFireTime = Time.time + 1.0f / fireRate;     //Set the next shot time.
            }    
        }    
    }
    void CheckPlayerInRange()
    {
        // Calculate the distance between the player and turret along the Z-axis only
        float distance = Mathf.Abs(player.position.z - turretHead.position.z);

        // Debugging log to verify the Z-distance
        //Debug.Log("Player Z Distance: " + distance);

        // If the player is within the detection range along the Z-axis, the turret can fire
        if (distance <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
            //Debug.Log("Player is not in range");
        }
    }
    void RotateTurretTowardsPlayer()
    {
        // Calculate the direction from the turret head to the player
        Vector3 direction = player.position - turretHead.position;

        // Calculate the rotation that would align the turret's forward direction with the player's position
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Apply smooth rotation to the turret head (rotates both barrels together)
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    // Fire projectiles towards the predicted position of the player
    void FireProjectiles()
    {
        // Calculate the predicted position of the player
        Vector3 predictedPlayerPosition = PredictPlayerPosition();

        // Instantiate projectiles for both barrels at the same time
        GameObject leftProjectile = Instantiate(projectilePrefab, leftFirePoint.position, leftFirePoint.rotation);
        GameObject rightProjectile = Instantiate(projectilePrefab, rightFirePoint.position, rightFirePoint.rotation);

        // Apply velocity to projectiles in the direction of the predicted position
        ApplyProjectileVelocity(leftProjectile, leftFirePoint.position, predictedPlayerPosition);
        ApplyProjectileVelocity(rightProjectile, rightFirePoint.position, predictedPlayerPosition);
    }

    // Apply velocity to a projectile based on the predicted position
    void ApplyProjectileVelocity(GameObject projectile, Vector3 firePointPosition, Vector3 predictedPosition)
    {
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply velocity towards the predicted position (normalized to make it consistent)
            rb.velocity = (predictedPosition - firePointPosition).normalized * projectileSpeed;
        }
    }

    // Predict the player's future position based on their current Z-axis position and a fixed lead time
    Vector3 PredictPlayerPosition()
    {
        // Get the player's current position
        Vector3 currentPlayerPosition = player.position;

        // Predict the player's future position along the Z-axis by adding a lead time (in seconds)
        float predictedZ = currentPlayerPosition.z + predictionLeadTime;

        // Set the predicted position in front of the player along the Z-axis
        return new Vector3(currentPlayerPosition.x, currentPlayerPosition.y, predictedZ);
    }
}