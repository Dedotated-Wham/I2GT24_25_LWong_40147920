using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltProjectileMoveForward : MonoBehaviour
{
    public float speed = 30.0f;

    private bool isHoming = false; // To check if homing behavior is activated
    private Vector3 forwardDirection;
    private HomingRocket homingRocketScript; // Reference to the HomingRocket script

    void Start()
    {
        forwardDirection = Vector3.forward;  // Save the initial facing direction

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        homingRocketScript = playerObject.GetComponentInParent<HomingRocket>();

        if (homingRocketScript == null)
        {
            Debug.LogError("HomingRocket script not found on player!");
        }

        StartCoroutine(SelfDestruct());
    }

    void Update()
    {
        if (isHoming)
        {
            // The homing behavior will take over and move the rocket
        }
        else
        {
            // Move forward along the initial forward direction
            transform.Translate(forwardDirection * Time.deltaTime * speed);
        }
    }

    // Activating homing mode
    public void ActivateHoming()
    {
        isHoming = true; // Mark homing behavior as active
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(4f); // Destroy object after x seconds

        // Check if we have a valid reference to the HomingRocket script and reset the launch flag
        if (homingRocketScript != null)
        {
            homingRocketScript.ResetLaunchFlag();  // Reset the launch flag on the HomingRocket script
        }

        Destroy(gameObject); // Destroy the rocket
    }
}