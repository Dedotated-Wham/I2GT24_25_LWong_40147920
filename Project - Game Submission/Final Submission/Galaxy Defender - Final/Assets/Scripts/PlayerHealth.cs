using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth;
    //public float amount = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float DamageTaken)
    {
        currentHealth -= DamageTaken;

        if(currentHealth <= 0)
        {
            // Player Dies
            Destroy(gameObject);          //Destroy Player Object when Health reaches 0.

            // Game Over Screen
            PlayerManager.isGameOver = true;
            Debug.Log("Using Player Health Script to End");
        }

    }
    
}
