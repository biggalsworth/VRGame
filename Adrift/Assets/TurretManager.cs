using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : ShipWeapon
{
    public Camera cam;
    public ShipCombat shipCombat;
    public ObjectPool bulletManager;
    GameObject bullet;

    public Transform shootPoint1;
    public Transform shootPoint2;
    public Vector3 target;
    public float speed = 1.0f;

    public float cooldown = 0.5f;
    public bool canShoot = true;
    IEnumerator coolOff;

    public LayerMask ignoreLayer;

    private void Start()
    {
        canShoot = true;
        coolOff = Cooldown();
        energy = maxEnergy;

        StartCoroutine(RegenerateEnergy());
    }

    void Update()
    {
        if (shipCombat.inCombat && active == true)
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

            // Calculate a rotation a step closer to the target and applies rotation to this object
            gameObject.transform.rotation = Quaternion.LookRotation(newDirection);

            //clamp x rotation
            //transform.eulerAngles = new Vector3(ClampAngle(transform.eulerAngles.x, transform.eulerAngles.x - 60, transform.eulerAngles.x + 60), 0, 0);

            //clamp y rotation
            //transform.eulerAngles = new Vector3(0, ClampAngle(transform.eulerAngles.y, transform.eulerAngles.y - 100, transform.eulerAngles.y + 100), 0);
            transform.eulerAngles = new Vector3(0, ClampAngle(transform.eulerAngles.y, -45, 90), 0);
        }
    }

    public override void Shoot()
    {
        if (canShoot && energy >= 10)
        {
            bullet = bulletManager.GetObject();

            playSound();
            energy -= 10f;
            Instantiate(bullet, shootPoint1.transform.position, shootPoint1.transform.rotation, null).GetComponent<Rigidbody>().velocity = shipCombat.gameObject.GetComponent<Rigidbody>().velocity;
            Instantiate(bullet, shootPoint2.transform.position, shootPoint2.transform.rotation, null).GetComponent<Rigidbody>().velocity = shipCombat.gameObject.GetComponent<Rigidbody>().velocity;

            StopCoroutine(coolOff);
            StartCoroutine(coolOff);
        }
    }


    private float ClampAngle(float angle, float min, float max, float rangemin = -180f, float rangemax = 180f)
    {
        /*
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
        */


        //We only want the angle in terms of -180 and 180
        //Wets say the angle is 270, thats the same as 90
        //We want the simplest angle to work with, from 0 to 180 and -180

        var modulus = Mathf.Abs(rangemax - rangemin);
        if ((angle %= modulus) < 0f) angle += modulus;
        return Mathf.Clamp(angle + Mathf.Min(rangemin, rangemax), min, max);
    }

    public override void ResetLook()
    {
        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
