using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Parameters")]
    public float maxHealth = 20;
    public float currentHealth;

    [Space]

    [Header("Explosion Prefab")]
    public GameObject explosionPrefabPlayer;

    [Space]

    [Header("Player Damaged Ship Prefab")]
    public GameObject damagedShip;

    //public float amount = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        damagedShip.SetActive(false);
    }

    public void TakeDamage(float DamageTaken)
    {
        currentHealth -= DamageTaken;

        if(currentHealth <= 0)
        {
            Instantiate(explosionPrefabPlayer, transform.position, transform.rotation);
            // Player Dies
            Destroy(gameObject);          //Destroy Player Object when Health reaches 0.

            // Game Over Screen
            PlayerManager.isGameOver = true;
            //Debug.Log("Using Player Health Script to End");
        }

        if (currentHealth <= 5)
        {
            damagedShip.SetActive(true);  // Enable the damaged ship object

            
        }
        else
        {
            damagedShip.SetActive(false);
        }

    }
    
}
