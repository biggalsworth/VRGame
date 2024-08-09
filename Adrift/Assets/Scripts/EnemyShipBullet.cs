using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBullet : MonoBehaviour
{
    private Rigidbody rb;

    public float damage = 10.0f;
    public float speed = 5.0f;

    public float lifetime = 10.0f;

    [HideInInspector]
    public IEnumerator lifeFunc;

    [HideInInspector]
    public bool available = true;   

    public GameObject sparkObject;
    public GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

        lifeFunc = countdown();
        //StartCoroutine(lifeFunc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship") && !available)
        {
            available = true;
            StartCoroutine(destroyBullet());
            if (GameObject.Find("Ship").GetComponent<ShipStats>() != null)
            {
                GameObject.Find("Ship").GetComponent<ShipStats>().TakeDamage(damage);
            }
        }
    }

    public IEnumerator destroyBullet()
    {
        StopCoroutine(lifeFunc);
        mesh.SetActive(false);
        sparkObject.SetActive(true);

        yield return new WaitForSeconds(4f);
        
        available = true;
        gameObject.SetActive(false);
        mesh.SetActive(true);
        sparkObject.SetActive(false);


    }

    public IEnumerator countdown()
    {
        yield return new WaitForSeconds(lifetime);
        StartCoroutine(destroyBullet());
    }
}
