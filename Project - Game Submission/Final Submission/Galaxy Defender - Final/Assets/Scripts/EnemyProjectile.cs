using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2.5f); //Delete Object after x seconds.
        Destroy(gameObject);
        //Debug.Log("Enemy Projectile Self Destructed");
    }
}
