using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    public float maxShieldHealth = 10;
    public float currentShieldHealth;

    private PlayerHealth playerHealth;              // Reference to the PlayerHealth script
    private PlayerController playerController;          //Referece to the PlayerController.                

    // Start is called before the first frame update
    void Start()
    {
        currentShieldHealth = 0;          //Initially no shield applied.
        playerHealth = GetComponent<PlayerHealth>(); // Get the PlayerHealth script attached to the same GameObject.
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasPowerUpShield)    // Check if the player has a shield
        {
            currentShieldHealth = maxShieldHealth;
            Debug.Log("Shield activated! Current shield health: " + currentShieldHealth);
        }
    }
}
