using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltProjectileMoveForward : MonoBehaviour
{
    public float speed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f); //Destroy object after x seconds.
        Destroy(gameObject);
        //Debug.Log("Projectile Self Destructed");
    }

}
