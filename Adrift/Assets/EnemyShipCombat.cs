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

    public EnemyBulletManager bulletSupply;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        bulletSupply = GameObject.Find("EnemyBulletManager").GetComponent<EnemyBulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (canShoot && GetComponent<EnemyShipStats>().dead == false)
        {
            GameObject currBullet = bulletSupply.GetBullet();
            currBullet.transform.position = shootPoint.transform.position;
            currBullet.transform.rotation = shootPoint.transform.rotation;

            currBullet.SetActive(true);

            currBullet.GetComponent<Rigidbody>().velocity = rb.velocity;
            currBullet.GetComponent<Rigidbody>().AddForce(currBullet.transform.forward * currBullet.GetComponent<EnemyShipBullet>().speed * 1.5f, ForceMode.Impulse);
            Debug.Log(GetComponent<Rigidbody>().velocity);


            StartCoroutine(ShotDelay());
        }
    }

    IEnumerator ShotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
