using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjClass : MonoBehaviour
{
    public bool available = true;

    private GameObject ship;

    private void Start()
    {
        ship = GameObject.Find("Ship");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(ship.transform.position, transform.position) > 200)
        {
            available = true;
            gameObject.SetActive(false);
        }
    }
}
