using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDrivingCollider : MonoBehaviour
{
    public ShipStats ship;

    public Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnedObject")
        {
            if (other.GetComponent<SpawnedObjClass>().type == ObjectType.Asteroid)
            {
                other.GetComponent<SpawnedObjClass>().currDurability = 0f;
                //other.gameObject.SetActive(false);
                ship.TakeDamage(10f);
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            //other.GetComponent<EnemyShipStats>().TakeDamage(10f);
            //other.GetComponent<Rigidbody>().AddForce(-other.transform.forward * 20f, ForceMode.Impulse);
            //ship.TakeDamage(10f);

        }
    }
}
