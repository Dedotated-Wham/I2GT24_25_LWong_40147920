using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;   //Reference to the Player Rigidbody.
    private GameObject focalPoint; //Get reference to focal point.
    public float speed = 5.0f;
    
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
    }
}
