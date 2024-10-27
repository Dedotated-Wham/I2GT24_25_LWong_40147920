using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public GameObject player;
    public float forwardSpeed = 5.0f;
    public float rollSpeed = 100.0f;
    public float verticalSpeed = 20.0f;
    public float horizontalSpeed = 20.0f;

    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;

    private float horizontalInput;
    private float verticalInput;
    public float rotateInput;

    //public Transform aimTarget;
    //public float lookSpeed = 1000;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Move plane forward.
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

        //Controls to move player plane.

        //Pitch plane
        //Press 'W' or 'S' to move up or down.
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * verticalSpeed, Space.World);

        //Yaw plane
        //Press 'A' or 'D' to move left or right.
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * horizontalSpeed, Space.World);

        //Roll plane.
        //Press 'Q' or 'E' to rotate along forward axis.

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * rollSpeed);

        }


        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * -rollSpeed);
        }

        //Fire projectile from player location.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
        }


        }
    }





