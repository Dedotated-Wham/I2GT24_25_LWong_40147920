using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);          //This will destroy the enemy object.
            Destroy(gameObject);                //This will destroy the player projectile.
            Debug.Log("Bullet Hit Enemy");
        }
    
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);          //This will destroy the obstacle.
            Destroy(gameObject);                //This will destroy the player projectile.
            Debug.Log("Bullet Hit Obstacle");
        }

        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);                //This will destroy the player projectile when it hits the environment.
            Debug.Log("Bullet Hit Obstacle");
        }
    }

 

 



}
