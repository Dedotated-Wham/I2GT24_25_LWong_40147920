using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DetectCollisionsEnemy : MonoBehaviour
{
    private PlayerHealth playerHealth;               //Reference PlayerHealth script.
    private ShieldHealth shieldHealth;          // Reference to the ShieldHealth script.
    private float damageTaken = 1f;

    [Header("Sound Manager")]
    public GameObject soundManager; // Reference to the SoundManager GameObject
    private AudioSource soundManagerAudioSource;
    public AudioClip playerHitSound;

    //private PlayerController shieldHealth;

    public void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        shieldHealth = GameObject.Find("Player").GetComponent<ShieldHealth>();
        // Get the AudioSource from the SoundManager GameObject
        soundManager = GameObject.Find("SoundManager"); // Find the SoundManager in the scene
        soundManagerAudioSource = soundManager.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //&& !hasPowerUpShield)
        {
            // Pass the damage to the ShieldHealth script to process the shield first.
            shieldHealth.ApplyDamage(damageTaken);

            soundManagerAudioSource.PlayOneShot(playerHitSound, 0.2f);
            Destroy(gameObject);                //Destroy Enemy Projectile on collision.
            //Debug.Log("Enemy Hit Player");

        }
    }
}