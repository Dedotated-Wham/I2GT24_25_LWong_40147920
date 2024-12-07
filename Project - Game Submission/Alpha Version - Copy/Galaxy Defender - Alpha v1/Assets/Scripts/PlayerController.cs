using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    private Transform playerModel;



    [Header("Parameters")]
    public float xySpeed = 10;
    public float lookSpeed = 50;
    public float forwardSpeed = 10;
    public float rollSpeed = 10;

    [Space]

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    [Space]

    [Header("Camera")]
    public Transform cameraParent;
    public Transform aimTarget;
    //public CinemachineDollyCart dolly;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        


        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);
        //HorizontalLean(playerModel, h, 20, 0.1f);
        ClampPosition();

        //Move plane forward.
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

        //***Code not working below, to be tested later on.***
        /*
        //Roll plane.
        //Press 'Q' or 'E' to rotate along forward axis.

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * rollSpeed);
            Debug.Log("Pressing Q");

        }


        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * -rollSpeed);
            Debug.Log("Pressing E");
        }
        */

        //Fire projectile from player location and direction player is facing.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);

        }
        // Move player around field of view camera.
        void LocalMove(float x, float y, float speed)
        {
            transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        }


        //Prevents the player from moving off screen.       
        void ClampPosition()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }


        //To improve the rotation of the plane as the player moves around.
        void RotationLook(float h, float v, float speed)
        {
            aimTarget.parent.position = Vector3.zero;
            aimTarget.localPosition = new Vector3(h, v, 1); //Choose an empty aim area in front of player for player object to follow.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed);
        }
        //***Code to be tested later on.***
        /*
        void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
        {
             Vector3 targetEulerAngles = target.localEulerAngles;
             target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
             //Debug.Log("HorizontalLeanWorking");
        }
          */

        //***Code not working below, to be tested later on.***

        /*
        void SetSpeed(float x)
        {
            dolly.m_Speed = x;
        }
        */
        /*
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(aimTarget.position, .5f);
            Gizmos.DrawSphere(aimTarget.position, .15f);

        }
         */
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("Player Crashed Into Enemy");
            //Debug.Break();
            PlayerManager.isGameOver = true;
            
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            Debug.Log("Player Crashed Into Obstacle");
            //Debug.Break();
            PlayerManager.isGameOver = true;

        }
        if (other.gameObject.tag == "End Level")
        {
            //Destroy(gameObject);
            Debug.Log("Player Reached The End Of Level");
            PlayerManager.isGameOver = true;
            Debug.Break();

        }

    }




}
