using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargoHandler : MonoBehaviour
{

    public ShipStats shipStats;

    public TextMeshProUGUI wealth;


    // Start is called before the first frame update
    void Start()
    {
        UpdateWealth();
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
        UpdateWealth();
    }

    void UpdateWealth()
    {
        wealth.text = "Current Wealth: \n" + shipStats.currentWealth.ToString();
    }
}
