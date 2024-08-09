using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnChecker : MonoBehaviour
{

    public GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("ShipParent");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ship.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpawnedObject")
        {
            other.gameObject.GetComponent<SpawnedObjClass>().Available();
        }
    }
}
