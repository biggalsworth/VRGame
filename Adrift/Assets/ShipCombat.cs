using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipCombat : MonoBehaviour
{
    BasicDrive driveScript;
    ShipStats shipStats;

    [HideInInspector]
    public bool inCombat;

    public GameObject CombatControlScreen;

    public InputActionReference shootButton;

    public TurretManager turret;

    // Start is called before the first frame update
    void Start()
    {
        inCombat = false;

        driveScript = gameObject.GetComponent<BasicDrive>();
        shipStats = gameObject.GetComponent<ShipStats>();

        CombatControlScreen.SetActive(false);

    }
    private void Awake()
    {
        shootButton.action.performed += Shoot;
    }

    private void OnEnable()
    {
        shootButton.action.performed += Shoot;
    }
    private void OnDisable()
    {
        shootButton.action.performed -= Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCombat)
        {
            if(driveScript.sitting == false)
            {
                ToggleCombat();
            }
        }
    }


    public void ToggleCombat()
    {
        inCombat = !inCombat;

        if(inCombat == true)
        {
            CombatControlScreen.SetActive(true);
        }
        else
        {
            CombatControlScreen.SetActive(false);
            turret.ResetLook();
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (inCombat)
        {
            turret.Shoot();
        }
    }
}
