using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

//using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    private Transform playerModel;
    private AudioSource playerAudio;
    private PlayerHealth playerHealth;               //Reference PlayerHealth script.
    private int CrashDamage = 2;

    //Power Up Fire Rate 

    public bool hasPowerUpFireRate;
    public float fireRateTimer = 5.0f;                   //Timer for Fire Rate Powerup.
    public float normalFireRate = 0.3f;                   //The time delay between main shots.
    //public float normalAltFireRate = 1.0f;               //The time delay between alt shots.
    public float powerUpFireRate = 0.15f;                //The time delay between shots.
    private float nextFire = 0.0f;                      //The time of the next shot for main weapon.
    //private float nextAltFire = 0.0f;                    //The time of the next shot for alt weapon.

    public float fireRateCountdownTime;                  // Track the remaining time of the power-up

    //Power Up Shield

    private ShieldHealthBar shieldHealthBar;           //Reference ShieldHealthBar script.
    public bool hasPowerUpShield;
    public int shieldHealth = 3;

    [Header("Parameters")]
    public float xySpeed = 10;
    public float lookSpeed = 50;
    public float forwardSpeed = 10;
    public float rollSpeed = 10;

    [Space]

    [Header("Projectile")]
    public GameObject projectilePrefab;
    //public GameObject altProjectilePrefab;
    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;
    //public Transform altProjectileSpawnPoint;

    [Space]

    [Header("Camera")]
    public Transform cameraParent;
    public Transform aimTarget;
    //public CinemachineDollyCart dolly;

    [Space]

    [Header("Sound Effects")]
    public AudioClip mainWeaponSound;
    //public AudioClip altWeaponSound;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        playerAudio = GetComponent<AudioSource>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        shieldHealthBar = GetComponent<ShieldHealthBar>();

        fireRateCountdownTime = fireRateTimer;      //Initialise the  fire rate power up timer.
        //UpdateCrosshairPosition();
        //hasPowerUpShield = false;                           
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

        //UpdateCrosshairPosition();


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

        //Fire main weapon projectile from player location and direction player is facing.
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            //Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
            nextFire = Time.time + normalFireRate;
            Shoot();
        }

        //Fire alternative weapon projectile from player location and direction player is facing.
        /*
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextAltFire)
        {
            //Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
            nextAltFire = Time.time + normalAltFireRate;
            AltShoot();
        }
        */

        //Method for main weapon.
        void Shoot()
        {
            Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.transform.rotation);
            Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointRight.transform.rotation);
            playerAudio.PlayOneShot(mainWeaponSound, 0.2f);

        }

        //Method for alternative weapon.
        /*
        void AltShoot()
        {
            Instantiate(altProjectilePrefab, altProjectileSpawnPoint.position, altProjectileSpawnPoint.transform.rotation);
            playerAudio.PlayOneShot(altWeaponSound, 0.2f);
        }
        */

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
            playerHealth.TakeDamage(CrashDamage);
            Debug.Log("Player Crashed Into Enemy");   
        }

        if (other.gameObject.tag == "Obstacle")
        {
            playerHealth.TakeDamage(CrashDamage);
            Debug.Log("Player Crashed Into Obstacle");
        }
        if (other.gameObject.tag == "End Level")
        {
            Debug.Log("Player Reached The End Of Level");
            PlayerManager.isLevelComplete = true;
        }

        if (other.gameObject.tag == "Power Up Fire Rate" && !hasPowerUpFireRate)
        {
            Destroy(other.gameObject);              //Destroy the power up object.
            hasPowerUpFireRate = true;
            normalFireRate = powerUpFireRate;     //Increase the fire rate by x seconds.
            
            Debug.Log("Player Collected Power Up");

            //Start Duration Countdown
            StartCoroutine(FireRateCountdown());
        }

        if (other.gameObject.tag == "Power Up Shield" && !hasPowerUpShield)
        {     
            hasPowerUpShield = true;
            Destroy(other.gameObject);              //Destroy the power up object.
            Debug.Log("Player Collected Power Up Shield");      
        }

        // If power up shield = 0 then set to false.
        //Destroy shield. make boolean false.

    }
    public IEnumerator FireRateCountdown()
    {
        Debug.Log("Starting Countdown");
        fireRateCountdownTime = fireRateTimer; // Reset the countdown time

        while (fireRateCountdownTime > 0)
        {
            fireRateCountdownTime -= Time.deltaTime;
                yield return null;
        }

        //yield return new WaitForSeconds(fireRateTimer);
        normalFireRate = 0.6f;                   //Using a float temporarily here but can improve and change to a declared float.
        hasPowerUpFireRate = false;
    }



}
