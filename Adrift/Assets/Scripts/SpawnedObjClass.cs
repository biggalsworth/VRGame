using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None,
    Asteroid
}

public class SpawnedObjClass : MonoBehaviour
{
    public bool available = true;

    private GameObject ship;

    public ObjectType type;

    public float durability = 10f;
    [HideInInspector]
    public float currDurability;

    private void Start()
    {
        currDurability = durability;
        ship = GameObject.FindGameObjectWithTag("ShipParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(ship.transform.position, transform.position) > 200)
        {
            available = true;
            gameObject.SetActive(false);
        }

        if(currDurability <= 0)
        {
            if(type == ObjectType.Asteroid)
            {
                ship.GetComponent<ShipStats>().GainItems(5.0f);
            }
            available = true;
            currDurability = durability;

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            //currDurability = 0;
            //ship.GetComponent<ShipStats>().TakeDamage(10f);
            //Debug.Log("HIT SHIP");
            //available = true;
        }
    }
}
