using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None,
    Asteroid,
    EnemyShip
}

public class SpawnedObjClass : MonoBehaviour
{
    public bool available = true;

    private GameObject ship;

    public ObjectType type;

    public float durability = 10f;
    [HideInInspector]
    public float currDurability;

    public bool randomiseValue = false;
    public float value = 200f;
    public float minVal = 10f;
    public float maxVal = 20f;
    public float weight = 10f;
                    

    private void Start()
    {
        currDurability = durability;
        ship = GameObject.FindGameObjectWithTag("ShipParent");

        if (randomiseValue)
        {
            value = Random.Range(minVal, maxVal);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(ship.transform.position, transform.position) > 250)
        {
            available = true;
            Available();
        }

        if (currDurability <= 0)
        {
            if (ship.GetComponent<ShipStats>().shipStorage + weight <= ship.GetComponent<ShipStats>().maxStorage)
            {
                ship.GetComponent<ShipStats>().cargoValue += value;

                if (type == ObjectType.Asteroid)
                {
                    ship.GetComponent<ShipStats>().GainItems(weight, value);
                    gameObject.GetComponent<Fracture>().FractureObject();
                }
                else if (type == ObjectType.EnemyShip)
                {
                    ship.GetComponent<ShipStats>().GainItems(weight, value);
                }
                else
                {
                    Available();
                }
                /*
                available = true;
                currDurability = durability;

                gameObject.SetActive(false);
                */
            }
        }
    }


    public void Available()
    {
        available = true;
        currDurability = durability;
        gameObject.SetActive(false);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "CollisionChecker" && other.gameObject.layer == LayerMask.NameToLayer("Ship") && other.tag != "Ignore")
        {
            //Available();
        }
    }
    
}
