using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipCombat : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    Rigidbody rb;

    public float cooldown = 1f;

    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Instantiate(bullet, shootPoint.position, shootPoint.rotation, null).GetComponent<Rigidbody>().velocity = rb.velocity;
            StartCoroutine(ShotDelay());
        }
    }

    IEnumerator ShotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot=true;
    }
}
