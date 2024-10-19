using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    private float speed = 20.0f;
    private float xRange = 25.0f;
    private float zRange = 20.0f;


    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Prevents player going off screen/map.
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < 0) 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }


        //Moving player horizontally.
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //Moving player vertically.
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        //Fire projectile from player location.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);




        }
    }
}
