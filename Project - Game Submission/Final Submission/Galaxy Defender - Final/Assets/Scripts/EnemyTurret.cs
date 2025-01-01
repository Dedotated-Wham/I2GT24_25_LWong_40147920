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
    public float rotationSpeed = 5.0f;      //Speed of the rotation for the Turret Head.
    public float detectionRange = 50.0f;    //The range at which the turret detects the player.
    public float projectileSpeed = 40.0f;

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
        //Calculate distance between the player and turret along the Z-axis.
        float distance = Vector3.Distance(new Vector3(player.position.x, player.position.y, 0), new Vector3(turretHead.position.x, turretHead.position.y, 0));
        // If the player is within the detection range, set the flag to true
        if (distance <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
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

    void FireProjectiles()
    {
        // Instantiate projectiles for both barrels at the same time
        GameObject leftProjectile = Instantiate(projectilePrefab, leftFirePoint.position, leftFirePoint.rotation);
        GameObject rightProjectile = Instantiate(projectilePrefab, rightFirePoint.position, rightFirePoint.rotation);

        // Get the Rigidbody of the projectiles and apply forward velocity to them
        Rigidbody leftRb = leftProjectile.GetComponent<Rigidbody>();
        if (leftRb != null)
        {
            // Manually apply the velocity in the forward direction of the fire point towards the player
            leftRb.velocity = (player.position - leftFirePoint.position).normalized * projectileSpeed;  // Adjust speed as needed
        }

        Rigidbody rightRb = rightProjectile.GetComponent<Rigidbody>();
        if (rightRb != null)
        {
            // Manually apply the velocity in the forward direction of the fire point towards the player
            rightRb.velocity = (player.position - rightFirePoint.position).normalized * projectileSpeed;  // Adjust speed as needed
        }
    }
}
