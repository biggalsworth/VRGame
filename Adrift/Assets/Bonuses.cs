using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public static Bonuses instance;
    DataManager dataSource;

    public float speedBonus = 1f;
    public float rotationBonus = 0f;
    public float shieldBonus = 0f;

    [HideInInspector]
    public int engineLevel = 1;
    [HideInInspector]
    public int stabiliserLevel = 1;
    [HideInInspector]
    public int shieldLevel = 1;

    public float storageCount;
    public float currValue;

    public float currHealth;
    public float currWealth;


    public ShipStats shipStats;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dataSource = gameObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currValue = shipStats.cargoValue;
        storageCount = shipStats.shipStorage;

        currHealth = shipStats.shipHealth;
        currWealth = shipStats.currentWealth;
    }

    public void UpdateData(List<string> bonuses)
    {
        int newbonus;

        // -- ENGINE/SPEED BONUS --

        speedBonus = ((Convert.ToInt32(bonuses[0]) - 1) * 5f);
        engineLevel = Convert.ToInt32(bonuses[0]);


        // -- ROTATION BONUS --

        newbonus = Convert.ToInt32(bonuses[1]);
        stabiliserLevel = newbonus;
        //if bonus level is 1
        if (newbonus == 1)
        {
            rotationBonus = 0f;
        }
        //if bonus level is 2
        else if( newbonus == 2)
        {
            rotationBonus = 0.25f;
        }
        //if bonus level is 3
        else if( newbonus == 3)
        {
            rotationBonus = 0.5f;
        }
        //if bonus level is 4
        else if( newbonus >= 4)
        {
            rotationBonus = 1f;
        }

        // -- SHIELD BONUS --

        newbonus = Convert.ToInt32(bonuses[2]);
        shieldLevel = newbonus;
        if (newbonus == 1)
        {
            shieldBonus = 0f;
        }
        //if bonus level is 2
        else if (newbonus == 2)
        {
            shieldBonus = 100f;
        }
        //if bonus level is 3
        else if (newbonus == 3)
        {
            shieldBonus = 150f;
        }
        //if bonus level is 4
        else if (newbonus  == 4)
        {
            shieldBonus = 200f;
        }

        storageCount = Convert.ToInt32(bonuses[3]);
        currValue = Convert.ToInt32(bonuses[4]);
        currHealth = Convert.ToInt32(bonuses[5]);
        currWealth = Convert.ToInt32(bonuses[6]);

        shipStats.shipStorage = storageCount;
        shipStats.speed += speedBonus;
        shipStats.cargoValue = currValue;
        shipStats.shipHealth = currHealth;
        shipStats.currentWealth = currWealth;

        

        if(shipStats.maxHealth == shipStats.shipHealth)
        {
            shipStats.maxHealth = 100f + shieldBonus;
            shipStats.shipHealth = shipStats.maxHealth;
        }
        else
        {
            shipStats.maxHealth = 100f + shieldBonus;
        }
    }
}
