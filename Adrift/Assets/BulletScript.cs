using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;

    public bool isEnemyBullet = false;

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
        if (!isEnemyBullet)
        {
            if (other.tag != "Player" && other.tag != "Ignore")
            {
                destroyBullet();
            }
            if (other.tag == "Enemy" && !isEnemyBullet)
            {
                destroyBullet();
                if (other.GetComponent<EnemyVital>() != null)
                {
                    other.GetComponent<EnemyVital>().RecieveDamage(damage);
                }
                else if(other.GetComponent<EnemyShipStats>() != null)
                {
                    other.GetComponent<EnemyShipStats>().TakeDamage(damage);
                }
            }
        }
        else
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ship"))
            {
                destroyBullet();
                if (GameObject.Find("Ship").GetComponent<ShipStats>() != null)
                {
                    GameObject.Find("Ship").GetComponent<ShipStats>().TakeDamage(damage);
                }
            }
            
            else if(other.gameObject.tag == "Player")
            {
                destroyBullet();
                other.GetComponent<PlayerStats>().TakeDamage(damage);
            }
            
        }
    }

    public void destroyBullet()
    {
        rb.velocity = Vector3.zero;
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
