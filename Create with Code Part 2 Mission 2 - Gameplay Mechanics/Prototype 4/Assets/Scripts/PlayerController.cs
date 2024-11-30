using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;   //Reference to the Player Rigidbody.
    private GameObject focalPoint; //Get reference to focal point.
    private float powerUpStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup = false; //Bool know if it's on or off.
    public GameObject powerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //Get the player Rigidbody.
        focalPoint = GameObject.Find("Focal Point"); //Find GameObject in Hierarchy.
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);   //Moves the player to the focal point locally rather than Vector3 globally.

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);   //Set powerup Indicator position equal to player's position.
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()          //IEnumerator (known as interface)
    {
        yield return new WaitForSeconds(7);         //Wait for x seconds.
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);    //Turn game object off.
    }


    private void OnCollisionEnter(Collision collision) //Use this to do something with physics.
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();  //Find enemy Rigidbody component.
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; //Setup new Vector3 for direction to send enemy to.

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); //Impulse, force applied instantly.
            Debug.Log("Collided with: " +  collision.gameObject.name + "with powerup set to " + hasPowerup);
        }
    }
}
