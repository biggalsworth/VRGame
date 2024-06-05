using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicDrive : MonoBehaviour
{
    public GameObject player;
    public Transform seatPos;
    public Transform dismountPos;

    public InputActionReference dismountButton;

    private bool sitting = false;

    [Header("Levers")]
    public GameObject throttle;
    public GameObject rotation;
    public Slider throttleSlider;

    private float rotationSpeed = 1.5f;

    private float currThrottle;
    private float currRotation;

    public TextMeshProUGUI speedTracker;
    [HideInInspector]
    public float currVelocity = 0.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        sitting = false;

        rb = GetComponent<Rigidbody>();
        //Make the ship static to start;
        //rb.constraints = RigidbodyConstraints.FreezeAll;

        //attach the player to the ship
        //player.transform.parent = gameObject.transform;

    }

    private void Awake()
    {
        dismountButton.action.performed += dismount;
    }

    private void OnEnable()
    {
        dismountButton.action.performed += dismount;
    }
    private void OnDisable()
    {
        dismountButton.action.performed -= dismount;
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ShipStats>().shipHealth > 10)
        {

            if (sitting)
            {
                currThrottle = throttleSlider.value;
                currRotation = rotation.GetComponent<ShipMovment>().rot;

                //if (Math.Abs(currThrottle) > 0.5f && Math.Abs(rb.velocity.magnitude) <= Math.Abs(currThrottle))
                if (Math.Abs(currThrottle) > 0.1f)// && Math.Abs(rb.velocity.magnitude - currThrottle) > 0.5f)
                {
                    //add forward direction based on seat orientation
                    //make sure to make the throttle increase with the ships allowed speed in ship stats
                    //*1000.0 to counteract the heavy mass
                    rb.AddForce(seatPos.transform.forward * (currThrottle * GetComponent<ShipStats>().speed) * 1000.0f, ForceMode.Force);
                }


                float rotDiff = Mathf.Abs(transform.eulerAngles.y - currRotation);
                //gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, currRotation, 0), rotationSpeed * Time.deltaTime);
                gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, currRotation, 0), Time.deltaTime * rotationSpeed);

            }
            else
            {
                rb.velocity = Vector3.zero;
                gameObject.isStatic = true;
            }

            currVelocity = rb.velocity.magnitude;
            currVelocity = Mathf.Round(currVelocity * 100) / 100;
            speedTracker.text = currVelocity.ToString();
        }
        else
        {
            rb.velocity = Vector3.zero;
            gameObject.isStatic = true;
        }
    }


    public void Sit()
    {
        if (!sitting)
        {
            gameObject.isStatic = false;

            //player.transform.parent = null;

            player.GetComponent<LocomotionSystem>().enabled = false;
            player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;


            player.transform.position = new Vector3(seatPos.position.x, seatPos.position.y - player.gameObject.GetComponent<CharacterController>().height/2, seatPos.position.z);
            player.transform.rotation = Quaternion.Euler(0, seatPos.rotation.y, 0);

            player.transform.parent = gameObject.transform;
            //player.transform.localPosition = Vector3.zero;

            //make sure the rigidbody can move
            rb.constraints = RigidbodyConstraints.None;
            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
            //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            //rb.constraints = RigidbodyConstraints.FreezePositionY;


            sitting = true;
        }
        
        else
        {

            player.transform.position = dismountPos.position;
            //player.transform.parent = gameObject.transform;
            player.transform.parent = null;

            player.GetComponent<LocomotionSystem>().enabled = true;
            player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;



            gameObject.isStatic = true;
            rb.velocity = Vector3.zero;
            throttle.GetComponent<ShipMovment>().speed = 0f;

            //make sure rigidbody does not move while not driving
            rb.constraints = RigidbodyConstraints.FreezeAll;

            sitting = false;
        }
        
        
    }

    public void ChangeAltitude(float force)
    {
        if (sitting && gameObject.GetComponent<ShipStats>().shipHealth > 10)
        {
            /*
            //reset velocity
            Vector3 vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;
            */
            rb.MovePosition(transform.position + (seatPos.up * force) * Time.deltaTime);
        }
    }
    
    private void dismount(InputAction.CallbackContext context)
    {
        if (sitting)
        {
            //Sit();
        }
    }
}
