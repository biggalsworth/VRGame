using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms.Impl;

public class ShipStats : MonoBehaviour
{

    public float maxHealth = 100;

    public float shipHealth;
    bool canHeal = true;
    public int damageSeverity = 0;
    public PartsHandler shipDamageHandler;

    public float maxStorage;
    [HideInInspector]
    public float shipStorage;
    public float cargoValue;

    public float speed = 1.5f;

    [Header("Lights")]
    [Tooltip("These lights will indicate warning signs")]
    public List<GameObject> ShipLights;
    public Color NormalLight;
    public Color DangerLight;
    bool CodeRedActive = false;

    // Start is called before the first frame update
    void Start()
    {
        shipHealth = maxHealth;
        CodeRedActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shipHealth <= 25 && CodeRedActive == false)
        {
            CodeRed(true);
        }
        if(shipHealth > 25 && CodeRedActive == true)
        {
            CodeRed(false);
        }

        if (shipHealth > 40 && canHeal)
        {
            StartCoroutine(Heal());
        }
        if (shipHealth < 40)
        {
            if(damageSeverity == 0)
            {
                //Debug.Log("SEVERE 1");

                damageSeverity = 1;
                shipDamageHandler.BreakPart();
            }
            if(shipHealth < 30 && damageSeverity < 2)
            {
                //Debug.Log("SEVERE 2");

                damageSeverity = 2; 
                shipDamageHandler.BreakPart();

            }
            if(shipHealth < 20 && damageSeverity < 3)
            {
                damageSeverity = 3;
                shipDamageHandler.BreakPart();

            }
        }
    }

    public void TakeDamage(float damage)
    {
        shipHealth -= damage;
        if (shipHealth < 0)
        {
            shipHealth = 0;
        }
    }


    public void GainItems(float amount)
    {
        if(shipStorage + amount <= maxStorage)
        {
            shipStorage += amount;
        }
        else
        {
            for(int i = 0; i < amount; i++)
            {
                if(shipStorage >= maxStorage)
                {
                    shipStorage = maxStorage;
                }
                else
                {
                    shipStorage++;
                }
            }
        }
    }

    public void CodeRed(bool danger)
    {
        if (danger)
        {
            CodeRedActive = true;
            foreach (GameObject obj in ShipLights)
            {
                obj.GetComponent<Light>().color = DangerLight;
                obj.GetComponent<Animator>().Play("Flashing");
            }
        }
        else
        {
            CodeRedActive = false;
            foreach (GameObject obj in ShipLights)
            {
                obj.GetComponent<Light>().color = NormalLight;
                obj.GetComponent<Animator>().Play("Idle");
            }
        }
    }


    public IEnumerator Heal()
    {
        canHeal = false;
        if (shipHealth < maxHealth)
        {
            shipHealth += 2.0f;
        }

        yield return new WaitForSeconds(5f);

        canHeal = true;
    }
}
