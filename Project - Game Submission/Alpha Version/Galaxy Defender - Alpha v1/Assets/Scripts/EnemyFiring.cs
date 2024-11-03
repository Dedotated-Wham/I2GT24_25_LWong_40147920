using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    public float speed = 30.0f;
    public float timer = 0.f;
    public float startDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemyProjectile", startDelay, timer);
    }

    void SpawnEnemyProjectile()
    {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);
        //transform.Translate(Vector3.up * Time.deltaTime * speed);

    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
