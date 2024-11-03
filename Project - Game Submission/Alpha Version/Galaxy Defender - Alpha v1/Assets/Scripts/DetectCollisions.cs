using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        //If Player Projectile comes in contact with enviroment or enemy, destroy it.
        if (other.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(gameObject);
            Debug.Log("Projectile Contact");
        }

        //If Player Projectile comes in contact with environment, destroy environment.
        if (other.gameObject.CompareTag("Destructible Environment"))
        {
            Destroy(gameObject);
            Debug.Log("Player Destroys Destructible Environment");
        }

        //If Player Projectile comes in contact with enemy, destroy enemy.
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Player Destroys Enemy");
        }

        //If Player Projectile comes in contact with environment or enemy, destroy player.
        //if (other.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
            //Debug.Log("Player Crashes");
        }

     

    }

 




}
