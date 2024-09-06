using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public ShipStats stats;

    public Bonuses bonus;

    [Header("UI Management")]
    public TextMeshProUGUI WealthText;

    public TextMeshProUGUI engineText;
    public TextMeshProUGUI enginePrice;

    public TextMeshProUGUI thrusterText;
    public TextMeshProUGUI thrusterPrice;

    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI shieldPrice;

    public TextMeshProUGUI minerText;
    public TextMeshProUGUI minerPrice;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindWithTag("ShipParent").GetComponent<ShipStats>();
    }

    // Update is called once per frame
    void Update()
    {
        WealthText.text = "Current Wealth: " + stats.currentWealth.ToString();

        engineText.text = "Current level = " + bonus.engineLevel.ToString();
        enginePrice.text = "Upgrade:\n" + (50 * (bonus.engineLevel + 1)).ToString();

        engineText.text = "Current level = " + bonus.stabiliserLevel.ToString();
        enginePrice.text = "Upgrade:\n" + (20 * (bonus.stabiliserLevel + 1)).ToString();

        shieldText.text = "Current level = " + bonus.shieldLevel.ToString();
        shieldPrice.text = "Upgrade:\n" + (115 * (bonus.shieldLevel + 1)).ToString();

        minerText.text = "Current level = " + bonus.minerLevel.ToString();
        minerPrice.text = "Upgrade:\n" + (100 * (bonus.minerLevel + 1)).ToString();
    }

    public void Upgrade(string name)
    {
        bonus = Bonuses.instance;
        if (name == "engine")
        {
            if(bonus.engineLevel < 5)
            {
                bonus.engineLevel += 1;
                stats.cargoValue -= 50 * (bonus.engineLevel + 1);
            }
        }
        if (name == "turn")
        {
            if(bonus.stabiliserLevel < 5)
            {
                bonus.stabiliserLevel += 1;
                stats.cargoValue -= 20 * (bonus.stabiliserLevel + 1);
            }
        }
        if (name == "shield")
        {
            if(bonus.shieldLevel < 5)
            {
                bonus.shieldLevel += 1;
                stats.cargoValue -= 100 * (bonus.shieldLevel + 1);
            }
        }
        if (name == "miner")
        {
            if(bonus.minerLevel < 5)
            {
                bonus.shieldLevel += 1;
                stats.cargoValue -= 100 * (bonus.shieldLevel + 1);
            }
        }
    }
    public void Downgrade(string name)
    {
        bonus = Bonuses.instance;
        if (name == "engine")
        {
            if(bonus.engineLevel > 0)
            {
                bonus.engineLevel -= 1;
            }
        }
        if (name == "turn")
        {
            if (bonus.stabiliserLevel > 0)
            {
                bonus.stabiliserLevel -= 1;
            }
        }
        if (name == "shield")
        {
            if (bonus.shieldLevel > 0)
            {
                bonus.shieldLevel -= 1;
            }
        }
        if (name == "miner")
        {
            if (bonus.minerLevel > 0)
            {
                bonus.minerLevel -= 1;
            }
        }
    }

    public void Cost(float cost)
    {
        stats.cargoValue -= cost;
    }
}
