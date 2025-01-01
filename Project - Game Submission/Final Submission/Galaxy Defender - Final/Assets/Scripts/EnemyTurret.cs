using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform player;                //Reference to the player's transform position.
    public Transform turretHead;            //Reference to the gun/barrel turretHead (child object with gun barrel to rotate).
    public GameObject projectilePrefab;     //Enemy projectile to use.
    public Transform leftFirePoint;         //Fire point for the left gun barrel. Part of the gun object.
    public Transform rightFirePoint;        //Fire point for the right gun barrel. Part of the gun object.
    public float fireRate = 1.0f;           //Time between shots.
    public float rotationSpeed = 5.0f;      //Speed of the rotation for the Turret Head.

    private float nextFireTime = 0f;        //Time when turret is allowed to fire again.



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
