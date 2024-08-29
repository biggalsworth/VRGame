using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShipCombat : MonoBehaviour
{
    BasicDrive driveScript;
    ShipStats shipStats;

    [HideInInspector]
    public bool inCombat;

    public GameObject CombatControlScreen;

    public InputActionReference shootButton;

    public ShipWeapon turret;

    public GameObject[] weapons;

    [Header("EMP")]
    public GameObject empObj;
    public AudioSource empSound;
    public float range;
    public float EMPCooldown = 20;
    public Button EMPButton;
    public TextMeshProUGUI EMPText;
    bool empAvailable = true;

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
        if (inCombat && shipStats.brokendown == false)
        {
            turret.Shoot();
        }
    }

    public void EMPBlast()
    {
        if(empAvailable)
        {
            StartCoroutine(EmpRoutine());
        }
    }

    IEnumerator EmpRoutine()
    {
        if (shipStats.brokendown == false)
        {
            empAvailable = false;
            EMPButton.interactable = false;
            EMPText.text = "RECHARGING";
            empSound.PlayOneShot(empSound.clip);

            empObj.GetComponent<ParticleSystem>().Play();

            foreach (RaycastHit hit in Physics.SphereCastAll(empObj.transform.position, range, Vector3.forward))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    Debug.Log("STUN");
                    StartCoroutine(hit.transform.gameObject.GetComponent<EnemyShipAI>().Freeze());
                }
            }

            yield return new WaitForSeconds(EMPCooldown);

            empAvailable = true;
            EMPButton.interactable = true;
            EMPText.text = "READY";
        }
    }
}
