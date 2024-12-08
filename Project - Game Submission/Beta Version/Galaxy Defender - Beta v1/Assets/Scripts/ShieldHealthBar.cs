using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHealthBar : MonoBehaviour
{   
    public ShieldHealth shieldHealth;               //Reference ShieldHealth script.
    public Image fillImage;
    private Slider slider;

    //private PlayerController player;         //Reference PlayerController script.
    public GameObject player;                 //Reference to the player object.
    void Awake()
    {

        if (player == null)
        {
            Debug.LogError("Player object is not assigned.");
        }

        slider = GetComponent<Slider>();
        //PlayerController holder = player.GetComponent<PlayerController>();
        fillImage.enabled = false;             //Shield Bar to be disabled at start before getting power up.
    }


    // Update is called once per frame
    void Update()
    {
        CheckForShield();
    }   
    void CheckForShield()
    {
        // Get the PlayerStatus component from the player
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            // Check if the player has the shield (hasPowerUpShield boolean is true)
            if (playerController.hasPowerUpShield)
            {
                fillImage.enabled = true;              //Allow bar colour to fill up.
                //Debug.Log("Player has a shield!");
                //ApplyShieldPowerUp();
            }
            else
            {
                fillImage.enabled = false;              //Disable bar colour when player does not have shield.
                // Handle the case when the player doesn't have a shield
                //Debug.Log("Player doesn't have a shield.");
            }
        }
        else
        {
            //Debug.LogError("PlayerStatus script not found on player object.");
        }
    }
    
        
}
