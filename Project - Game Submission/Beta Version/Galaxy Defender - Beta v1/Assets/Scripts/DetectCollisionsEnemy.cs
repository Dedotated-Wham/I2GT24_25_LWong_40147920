using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsEnemy : MonoBehaviour
{
    private PlayerHealth playerHealth;               //Reference PlayerHealth script.
    private int DamageTaken = 5;

    //private PlayerController shieldHealth;

    public void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        //shieldHealth = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //&& !hasPowerUpShield)
        {
            playerHealth.TakeDamage(DamageTaken); 
            
            //Destroy(other.gameObject);          //Destroy Player Object on collision.
            Destroy(gameObject);                //Destroy Enemy Projectile on collision.
            Debug.Log("Enemy Hit Player");
            //PlayerManager.isGameOver = true;       //Moved to PlayerHealth script.
        }
    }
}
