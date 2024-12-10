using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 35.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
        
    }

    // Update is called once per frame
    void Update()
    {
    //Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);
    transform.Translate(Vector3.up * Time.deltaTime * speed);
    }    

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2.5f); //Delete Object after x seconds.
        Destroy(gameObject);
        //Debug.Log("Enemy Projectile Self Destructed");
    }
}
