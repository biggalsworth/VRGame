using System;
using UnityEngine;
using UnityEngine.UI;

public class CombatScreen : MonoBehaviour
{
    public GameObject weaponScreen;
    public GameObject helpScreen;

    public ShipCombat combatScript;

    GameObject currWeapon;
    ShipWeapon currScript;


    public Image energyImage;

    // Start is called before the first frame update
    void Start()
    {
        currWeapon = combatScript.weapons[0];
        currScript = currWeapon.GetComponent<ShipWeapon>();
        currScript.active = true;

        weaponScreen.SetActive(true);
        helpScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //make sure to round it to 2 d.p.
        energyImage.fillAmount = (float)Math.Round(currScript.energy / currScript.maxEnergy, 2);
    }

    public void ShowHelp()
    {
        weaponScreen.SetActive(!weaponScreen.activeSelf);
        helpScreen.SetActive(!helpScreen.activeSelf);
    }

    public void SwapWeapon(int weaponID)
    {
        currWeapon.GetComponent<ShipWeapon>().active = false;
        currWeapon = combatScript.weapons[weaponID];

        combatScript.turret = currWeapon.GetComponent<ShipWeapon>();
        currScript = currWeapon.GetComponent<ShipWeapon>();
        currScript.active = true;



    }
}
