using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Rigidbody rb;

    public float damage = 10.0f;
    public float speed = 5.0f;

    public float lifetime = 10.0f;

    public GameObject deathEffect;

    IEnumerator lifeFunc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

        lifeFunc = countdown();
        StartCoroutine(lifeFunc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" && other.tag != "Ignore")
        {
            Debug.Log(other.transform.name);
            destroyBullet();
        }
        if(other.tag == "Enemy")
        {
            destroyBullet();
            if (other.GetComponent<EnemyVital>() != null)
            {
                other.GetComponent<EnemyVital>().RecieveDamage(damage);
            }
            else
            {
                other.GetComponent<EnemyShipStats>().TakeDamage(damage);
            }
        }
    }

    public void destroyBullet()
    {
        StopCoroutine(lifeFunc);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(lifetime);
        destroyBullet();
    }
}
