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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            destroyBullet();
        }
        if(other.tag == "Enemy")
        {
            destroyBullet();
            other.GetComponent<EnemyVital>().RecieveDamage(damage);
        }
    }

    public void destroyBullet()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(lifetime);
        destroyBullet();
    }
}
