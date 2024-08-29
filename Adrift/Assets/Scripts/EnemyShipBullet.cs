using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBullet : MonoBehaviour
{
    private Rigidbody rb;

    public float damage = 5.0f;
    public float speed = 5.0f;

    public float lifetime = 10.0f;

    [HideInInspector]
    public IEnumerator lifeFunc;

    [HideInInspector]
    public bool available = true;   

    public GameObject sparkObject;
    public GameObject mesh;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lifeFunc = countdown();

        mesh.SetActive(true);
        sparkObject.SetActive(false);
    }


    private void OnEnable()
    {
        StopAllCoroutines();

        lifeFunc = countdown();
        StartCoroutine(lifeFunc);

        mesh.SetActive(true);
        sparkObject.SetActive(false);

    }

    private void OnDisable()
    {
        mesh.SetActive(true);
        sparkObject.SetActive(false);

        StopCoroutine(lifeFunc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship") && !available)
        {
            if (GameObject.FindGameObjectWithTag("ShipParent").GetComponent<ShipStats>() != null)
            {
                GameObject.FindGameObjectWithTag("ShipParent").GetComponent<ShipStats>().TakeDamage(damage);
            }
            StartCoroutine(destroyBullet());
        }
    }
        
    public IEnumerator destroyBullet()
    {
        available = true;
        mesh.SetActive(false);
        sparkObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        mesh.SetActive(true);
        sparkObject.SetActive(false);
        available = true;
        gameObject.SetActive(false);


    }

    public IEnumerator countdown()
    {
        yield return new WaitForSeconds(lifetime);
        StartCoroutine(destroyBullet());
    }
}
