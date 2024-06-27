using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public Camera cam;
    public ShipCombat shipCombat;
    public GameObject bullet;

    [HideInInspector]
    public Transform shootPoint;
    public Vector3 target;
    public float speed = 1.0f;

    public LayerMask ignoreLayer;

    private void Start()
    {

    }

    void Update()
    {
        if (shipCombat.inCombat)
        {
            Debug.Log("begin raycast");
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 50f, ~ignoreLayer)) 
            {
                target = hit.point;
            }
            else
            {
                target = cam.transform.position + cam.transform.forward * 50;
            }


            Debug.DrawRay(cam.transform.position, target, Color.yellow);


            // Determine which direction to rotate towards
            Vector3 targetDirection = target - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            transform.eulerAngles = new Vector3(ClampAngle(transform.eulerAngles.x, transform.eulerAngles.x - 60, transform.eulerAngles.x + 60), 0, 0);
            transform.eulerAngles = new Vector3(0, ClampAngle(transform.eulerAngles.y, transform.eulerAngles.y - 100, transform.eulerAngles.y + 100), 0);


            // Calculate a rotation a step closer to the target and applies rotation to this object
            gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation, null).GetComponent<Rigidbody>().velocity = shipCombat.gameObject.GetComponent<Rigidbody>().velocity;
    }


    private float ClampAngle(float angle, float min, float max)
    {
        Debug.Log(angle);
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }

    internal void ResetLook()
    {
        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
