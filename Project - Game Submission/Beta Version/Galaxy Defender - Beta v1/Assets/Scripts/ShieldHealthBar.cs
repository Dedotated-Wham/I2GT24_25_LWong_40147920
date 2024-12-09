using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHealthBar : MonoBehaviour
{
    public ShieldHealth shieldHealth;         // Reference to ShieldHealth script
    public Image fillImage;                   // Reference to the fill Image component of the shield bar
    private Slider slider;                    // Reference to the Slider component

    public GameObject player;                 // Reference to the player object

    void Awake()
    {
        // Try to get the Slider component attached to this GameObject
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("Slider component is missing on this GameObject!");
        }

        fillImage.enabled = false;  // Shield Bar is hidden until the player gets the shield power-up
    }

    void Update()
    {
        // If shieldHealth is missing, exit early
        if (shieldHealth == null)
        {
            Debug.LogError("ShieldHealth script reference is missing!");
            return;
        }

        // Check for shield status and update the shield health slider
        UpdateShieldBar();

        // If the shield is depleted (value is 0), hide the shield bar
        if (slider.value <= 0f)
        {
            fillImage.enabled = false;
        }

        // Check if the player has the shield power-up, and show the shield bar
        CheckForShield();
    }

    // Method to update the shield bar based on current shield health
    void UpdateShieldBar()
    {
        // Ensure the slider's value stays between 0 and 1 (for visual representation)
        float fillValue = shieldHealth.currentShieldHealth / shieldHealth.maxShieldHealth;
        slider.value = Mathf.Clamp(fillValue, 0f, 1f);  // Clamps value to [0, 1]

        // Update the fill image's fill amount (progress bar visualization)
        fillImage.fillAmount = fillValue;
    }

    // Check if the player has a shield and update the shield bar visibility accordingly
    void CheckForShield()
    {
        // Check if player object is assigned
        if (player == null)
        {
            Debug.LogError("Player reference is missing!");
            return;
        }

        // Try to get PlayerController from the player object
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            // If the player has the shield, show the shield bar
            if (playerController.hasPowerUpShield)
            {
                fillImage.enabled = true;  // Display shield bar when the shield is active
            }
            else
            {
                fillImage.enabled = false;  // Hide shield bar when no shield is active
            }
        }
        else
        {
            // If PlayerController is missing on the player object
            Debug.LogError("PlayerController script not found on player object.");
        }
    }
}
