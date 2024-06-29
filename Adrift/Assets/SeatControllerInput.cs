using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeatControllerInput : MonoBehaviour
{
    public InputActionReference sitButton;
    public BasicDrive ship;

    GameObject player; 


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ship.Sit();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            ship.Sit();
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
