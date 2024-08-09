using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicSeat : MonoBehaviour
{
    public InputActionReference sitButton;

    GameObject player;

    bool sitting = false;

    public Transform sitDownPosition;
    public Transform dismountPos;


    // Start is called before the first frame update
    void Start()
    {
        player = null;
    }

    private void Awake()
    {
        sitButton.action.performed += mount;
    }

    private void OnEnable()
    {
        sitButton.action.performed += mount;
    }
    private void OnDisable()
    {
        sitButton.action.performed -= mount;
    }

    private void mount(InputAction.CallbackContext context)
    {
        if (player != null)
        {
            if (!sitting)
            {
                sitting = true;
                player.transform.position = transform.position;
                player.GetComponent<LocomotionSystem>().enabled = false;
                player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
                player.GetComponent<CharacterController>().enabled = false;
            }
            else
            {
                sitting = false;
                player.transform.position = dismountPos.position;
                player.GetComponent<LocomotionSystem>().enabled = true;
                player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

}
