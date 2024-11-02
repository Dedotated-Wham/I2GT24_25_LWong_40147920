using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerModel;

    public float xySpeed = 10;
    public float lookSpeed = 250;
    public float forwardSpeed = 10;

    public Transform cameraParent;
    public Transform aimTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        //SetSpeed(forwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");




        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);
        //HorizontalLean(playerModel, h, 40, 0.1f);
        ClampPosition();

        //Move plane forward.
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

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

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1); //Choose an aim area in front of player object.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed);
    }
    /*
    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }
      */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }

}
