using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoHandler : MonoBehaviour
{

    public ShipStats shipStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Sell()
    {
        shipStats.currentWealth += shipStats.cargoValue;
        shipStats.cargoValue = 0;
        shipStats.shipStorage = 0;
    }
}
