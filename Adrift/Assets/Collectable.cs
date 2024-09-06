using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    ShipStats stats;

    public float minValue = 5;
    public float maxValue = 10;
    float value;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindWithTag("ShipParent").GetComponent<ShipStats>();
        value = Random.Range(minValue, maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Collected()
    {
        stats.cargoValue += value;
        Destroy(gameObject);
    }
}
