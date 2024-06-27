using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;
    public GameObject fracturedEffect;

    public void FractureObject()
    {
        Instantiate(fracturedEffect, transform.position, transform.rotation); //Spawn in the broken version
        gameObject.GetComponent<SpawnedObjClass>().Available();
        //Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}
