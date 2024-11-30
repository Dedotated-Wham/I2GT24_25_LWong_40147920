using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;                      //Declare a Rigidbody variable.
    private GameObject player;                             //Declare the player gameObject variable.
    // Start is called before the first frame update
    void Start()
    {
            enemyRb = GetComponent<Rigidbody>(); //Declare/Get reference of Rigidbody.
            player = GameObject.Find("Player");    //Need to find the player variable.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;    //Direction enemy moves towards.
        //normalized makes the vector a constant speed so it does not increase over time or distance.

        enemyRb.AddForce(lookDirection * speed); 
            
            //Add Force (calculates vector based on player and enemy position.
            //player.transform.position to get current position of player.
            //transform.position is the position of the enemy. (This script)
    }
}
