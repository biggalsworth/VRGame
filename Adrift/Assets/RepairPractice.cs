using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairPractice : ShipStats
{
    //public float shipHealth;
    //public float damageSeverity;

    public PartsHandler damageHandler;

    public TextMeshProUGUI healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shipHealth < 40)
        {
            if (damageSeverity == 0)
            {
                damageSeverity = 1;
                damageHandler.BreakPart();
            }
            if (shipHealth < 30 && damageSeverity < 2)
            {
                //Debug.Log("SEVERE 2");

                damageSeverity = 2;
                damageHandler.BreakPart();

            }
            if (shipHealth < 20 && damageSeverity < 3)
            {
                damageSeverity = 3;
                damageHandler.BreakPart();

            }
        }

        healthDisplay.text = shipHealth.ToString();
    }

    public void SetHealth(float newHealth)
    {
        shipHealth = newHealth;
    }
}
