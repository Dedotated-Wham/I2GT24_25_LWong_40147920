using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /*
    [Header("Parameters")]

    public Transform player;            //Reference to the player game object.
    public float detectionRange = 20.0f; //Range/Distance for enemy to detect the player.
    public float rotationSpeed = 3.0f;   //Enemy rotation speed to target player.
    public GameObject projectilePrefab;  // The projectile prefab to fire
    public Transform firePoint;          // The point from which the projectile is fired. Projectile Spawn Point.
    public float fireRate = 0.5f;          // Rate of fire (shots per second)

    private float nextFireTime = 0f;     // Time tracking the next fire
    private bool isPlayerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)        //If player is in range then fire.
        {
            isPlayerInRange = true;

            RotateTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1.0f / fireRate; // Set the next time to fire
            }
        }

        else
        {
            isPlayerInRange = false;         //Player is not in range.
        }
    }

    public void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void FireProjectile()
    {
        if (projectilePrefab && firePoint)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
    */
}
