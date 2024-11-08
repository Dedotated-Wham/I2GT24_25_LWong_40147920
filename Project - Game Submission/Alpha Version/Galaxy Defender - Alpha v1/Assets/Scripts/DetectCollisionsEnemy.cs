using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);          //Destroy Player Object on collision.
            Destroy(gameObject);                //Destroy Enemy Projectile on collision.
            Debug.Log("Enemy Hit Player");
            Debug.Break();
        }
    }
}
