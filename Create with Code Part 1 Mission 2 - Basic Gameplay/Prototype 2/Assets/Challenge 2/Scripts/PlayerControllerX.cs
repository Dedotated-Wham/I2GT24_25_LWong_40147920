using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float cooldown = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press and condition met for cooldown, send dog
        if (Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            cooldown = 1.0f;
        }
        // Reset cooldown timer per frame
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
    
}

