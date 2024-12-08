using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsEnemy : MonoBehaviour
{
    //private PlayerController shieldHealth;

    public void Start()
    {
        //shieldHealth = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //&& !hasPowerUpShield)
        {
            Destroy(other.gameObject);          //Destroy Player Object on collision.
            Destroy(gameObject);                //Destroy Enemy Projectile on collision.
            Debug.Log("Enemy Hit Player");
            //Debug.Break();
            PlayerManager.isGameOver = true;
        }
    }
}
