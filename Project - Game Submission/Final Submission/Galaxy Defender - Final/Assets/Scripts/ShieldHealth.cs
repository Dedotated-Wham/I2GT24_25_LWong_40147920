using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    public float maxShieldHealth = 10;               //Max Shield Health float.
    public float currentShieldHealth;               //Current Shield Health.

    private PlayerHealth playerHealth;              // Reference to the PlayerHealth script
    private PlayerController playerController;          //Referece to the PlayerController.
    private bool shieldActivated = false;         // Boolean to ensure the shield is only activated once, boolean only in this script.

    // Start is called before the first frame update
    void Start()
    {
        currentShieldHealth = 0;          //Initially no shield applied.
        playerHealth = GetComponent<PlayerHealth>(); // Get the PlayerHealth script attached to the same GameObject.
        playerController = GetComponent<PlayerController>(); // Get the PlayerController script attached to the same GameObject.
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerController.hasPowerUpShield && !shieldActivated)    // Check if the player has a shield activated boolean and the Power Up Shield boolean from another script.
        {
            currentShieldHealth = maxShieldHealth;          //Set shield health to max.
            shieldActivated = true;  // Mark shield as activated
            //Debug.Log("Shield activated! Current shield health: " + currentShieldHealth);
        }

        if (currentShieldHealth <= 0f && playerController.hasPowerUpShield)     //If player loses shield and still has the power up shield boolean from another script, turn it off.
        {
            playerController.hasPowerUpShield = false;  // Deactivate the shield power-up boolean from another script.
            shieldActivated = false;                    //Deactivate the shield activated boolean from this script.
            Debug.Log("Shield is depleted. Shield power-up is now inactive.");
        }
        
    }

    // This method will be called from DetectCollisionsEnemy to apply damage.
    public void ApplyDamage(float damageAmount)
    {
        // Check if the player has a shield and it's still active.
        if (playerController.hasPowerUpShield && currentShieldHealth > 0f)
        {
            // Calculate how much damage the shield will absorb (whichever is smaller: remaining shield health or incoming damage).
            float damageToShield = Mathf.Min(damageAmount, currentShieldHealth);

            // Reduce the shield health by the absorbed damage.
            currentShieldHealth -= damageToShield;

            // Subtract the absorbed damage from the incoming damage.
            damageAmount -= damageToShield;

            Debug.Log("Shield absorbed " + damageToShield + " damage. Remaining shield health: " + currentShieldHealth);

        }

        // If there is any remaining damage, apply it to the player's health.
        if (damageAmount > 0f)
        {
            playerHealth.TakeDamage(damageAmount);  // Pass the remaining damage to the PlayerHealth script.
            Debug.Log("Remaining damage applied to player: " + damageAmount);
        }
    }
}