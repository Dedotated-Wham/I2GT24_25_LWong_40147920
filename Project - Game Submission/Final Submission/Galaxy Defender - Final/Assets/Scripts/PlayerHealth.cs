using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Parameters")]
    public float maxHealth = 100;
    public float currentHealth;

    [Space]

    [Header("Explosion Prefab")]
    public GameObject explosionPrefabPlayer;

    [Space]

    [Header("Player Damaged Ship Prefab")]
    public GameObject damagedShip;



    [Space]

    [Header("Sound Manager")]
    public GameObject soundManager; // Reference to the SoundManager GameObject
    private AudioSource soundManagerAudioSource;
    public AudioClip playerDestructionSound;
    // Reference to the Player's AudioSource and Low Health Sound
    private AudioSource playerAudio;
    public AudioClip playerLowHealthSound;
    public float lowHealthSoundVolume = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        damagedShip.SetActive(false);

        // Get the AudioSource from the SoundManager GameObject
        soundManagerAudioSource = soundManager.GetComponent<AudioSource>();

        //Get the AudioSource from the Player GameObject.
        playerAudio = GetComponent<AudioSource>();
    }

    public void TakeDamage(float DamageTaken)
    {
        currentHealth -= DamageTaken;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            soundManagerAudioSource.PlayOneShot(playerDestructionSound, 0.3f);

            Instantiate(explosionPrefabPlayer, transform.position, transform.rotation);

            // Player Dies
            Destroy(gameObject);          //Destroy Player Object when Health reaches 0.
            Debug.Log("Using Player Health Script to End");
            // Game Over Screen
            PlayerManager.isGameOver = true;
            
        }

        if (currentHealth <= 10)
        {
            damagedShip.SetActive(true);  // Enable the damaged ship object
            playerAudio.PlayOneShot(playerLowHealthSound, 0.2f);


        }
        else
        {
            damagedShip.SetActive(false);
        }

    }


}
