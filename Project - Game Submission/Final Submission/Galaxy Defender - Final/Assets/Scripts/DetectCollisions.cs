using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;         //Reference GameManager script and call it gameManager inside this script.
    private int scoreToAdd = 1;
    private AudioSource projectileAudio;

    public GameObject explosionPrefabEnemy;
    public GameObject explosionPrefabObstacle;

    [Header("Sound Effects")]

    public AudioClip obstacleSound;
    public AudioClip enemySound;

  


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();         //Find Game Manager object and find it's game manager script.
        projectileAudio = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //projectileAudio.PlayOneShot(enemySound, 5.0f);       //To be checked.                                      
            Instantiate(explosionPrefabEnemy, transform.position, transform.rotation);  //Instantiate explosion on enemy object position.
            Destroy(other.gameObject);              //This will destroy the enemy object.
            Destroy(gameObject);                    //This will destroy the player projectile.
            gameManager.UpdateScore(scoreToAdd);    //Add score to the game manager script whenever an enemy is destroyed.   
            Debug.Log("Bullet Hit Enemy");
        }
    
        if (other.gameObject.tag == "Obstacle")
        {
            //projectileAudio.PlayOneShot(obstacleSound, 5.0f);       //To be checked.
            Instantiate(explosionPrefabObstacle, transform.position, transform.rotation);  //Instantiate explosion on enemy object position.
            Destroy(gameObject);                //This will destroy the player projectile.
            Destroy(other.gameObject);          //This will destroy the obstacle.

            Debug.Log("Bullet Hit Obstacle");
        }

        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);                //This will destroy the player projectile when it hits the environment.
            Debug.Log("Bullet Hit Environment");
        }
    }

 

 



}
