using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;

    public bool isEnemyBullet = false;
    public bool isShipBullet = false;

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

    private void OnEnable()
    {
        StopCoroutine(lifeFunc);
        lifeFunc = countdown();
        StartCoroutine(lifeFunc);
    }

    private void OnDisable()
    {
        StopCoroutine(lifeFunc);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject obj = other.gameObject;
        if (!isEnemyBullet)
        {
            if (obj.tag != "Player" && obj.tag != "Ignore")
            {
                destroyBullet();
            }
            if (obj.tag == "Enemy" && !isEnemyBullet)
            {
                destroyBullet();
                if (obj.GetComponent<EnemyVital>() != null)
                {
                    obj.GetComponent<EnemyVital>().RecieveDamage(damage);
                }
                else if(obj.GetComponent<EnemyShipStats>() != null)
                {
                    obj.GetComponent<EnemyShipStats>().TakeDamage(damage);
                }
            }
        }
        else
        {
            if (obj.layer == LayerMask.NameToLayer("Ship"))
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
                obj.GetComponent<PlayerStats>().TakeDamage(damage);
            }
            
        }
    }

    public void destroyBullet()
    {
        rb.velocity = Vector3.zero;
        StopCoroutine(lifeFunc);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (!isShipBullet)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(lifetime);
        destroyBullet();
    }
}
