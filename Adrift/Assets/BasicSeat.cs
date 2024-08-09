using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicSeat : MonoBehaviour
{
    public InputActionReference sitButton;

    GameObject player;
    GameObject locomotion;

    bool sitting = false;

    public Transform sitDownPosition;
    public Transform dismountPos;


    // Start is called before the first frame update
    void Start()
    {
        player = null;
        locomotion = GameObject.Find("LocomotionSystem");
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
                //player.transform.position = transform.position;
                player.transform.position = sitDownPosition.transform.position;
                player.transform.rotation = sitDownPosition.transform.rotation;

                locomotion.GetComponent<LocomotionSystem>().enabled = false;
                locomotion.GetComponent<ContinuousMoveProviderBase>().enabled = false;
                player.GetComponent<CharacterController>().enabled = false;
            }
            else
            {
                sitting = false;
                //player.transform.position = dismountPos.position;
                player.transform.position = dismountPos.transform.position;
                player.transform.rotation = dismountPos.transform.rotation;

                locomotion.GetComponent<LocomotionSystem>().enabled = true;
                locomotion.GetComponent<ContinuousMoveProviderBase>().enabled = true;
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
