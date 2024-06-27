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
        
    }

    public void UpdateBonuses(string bonuses)
    {
        int newbonus;

        speedBonus = ((Convert.ToInt32(bonuses[0]) - 1) * 20f);
        engineLevel = Convert.ToInt32(bonuses[0]);
        // -- ROTATION BONUS --

        newbonus = Convert.ToInt32(bonuses[1]);
        stabiliserLevel = newbonus;
        //if bonus level is 1
        if (newbonus < 2)
        {
            rotationBonus = 0f;
        }
        //if bonus level is 2
        else if( newbonus < 3)
        {
            rotationBonus = 0.25f;
        }
        //if bonus level is 3
        else if( newbonus < 4)
        {
            rotationBonus = 0.5f;
        }
        //if bonus level is 4
        else if( newbonus < 5)
        {
            rotationBonus = 1f;
        }

        // -- SHIELD BONUS --

        newbonus = Convert.ToInt32(bonuses[2]);
        shieldLevel = newbonus;
        if (newbonus < 2)
        {
            shieldBonus = 0f;
        }
        //if bonus level is 2
        else if (newbonus < 3)
        {
            shieldBonus = 100f;
        }
        //if bonus level is 3
        else if (newbonus < 4)
        {
            shieldBonus = 150f;
        }
        //if bonus level is 4
        else if (newbonus < 5)
        {
            shieldBonus = 200f;
        }

        if(Mathf.Abs(shipStats.maxHealth - shipStats.shipHealth) < 10)
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
