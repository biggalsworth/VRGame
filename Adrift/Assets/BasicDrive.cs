using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private float rotationSpeed = 50;

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
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //attach the player to the ship
        player.transform.parent = gameObject.transform;

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
        if (sitting)
        {
            //currThrottle = throttle.GetComponent<HingeJoint>().angle;
            //currRotation = Math.Abs(rotation.GetComponent<HingeJoint>().angle);

            currThrottle = throttle.GetComponent<ShipMovment>().speed;
            currRotation = rotation.GetComponent<ShipMovment>().rot;

            if (Math.Abs(currThrottle) > 0.5f && Math.Abs(rb.velocity.magnitude) < Math.Abs(currThrottle))
            {
                rb.AddForce(transform.forward * currThrottle, ForceMode.Force);
            }

            if(Math.Abs(currThrottle) == 0f)
            {
                rb.velocity = Vector3.zero;
            }


            float rotDiff = Mathf.Abs(transform.eulerAngles.y - currRotation);
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, currRotation, 0), rotationSpeed * Time.deltaTime);

            currVelocity = rb.velocity.magnitude;
        }

        speedTracker.text = currThrottle.ToString();
    }


    public void Sit()
    {
        if (!sitting)
        {
            //player.transform.parent = null;

            player.GetComponent<LocomotionSystem>().enabled = false;
            player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;


            player.transform.position = new Vector3(seatPos.position.x, seatPos.position.y - player.gameObject.GetComponent<CharacterController>().height/2, seatPos.position.z);
            player.transform.rotation = Quaternion.Euler(0, seatPos.rotation.y, 0);

            //  player.transform.parent = seatPos.transform;
            //player.transform.localPosition = Vector3.zero;

            //make sure the rigidbody can move
            //rb.constraints = RigidbodyConstraints.None;
            //rb.constraints = RigidbodyConstraints.FreezePositionY;
            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
            //rb.constraints = RigidbodyConstraints.FreezeRotationZ;

            sitting = true;
        }
        else
        {
            rb.velocity = Vector3.zero;
            throttle.GetComponent<ShipMovment>().speed = 0f;

            player.GetComponent<LocomotionSystem>().enabled = true;
            player.GetComponent<ContinuousMoveProviderBase>().enabled = true; 
            player.GetComponent<CharacterController>().enabled = true;

            player.transform.position = dismountPos.position;
            player.transform.parent = gameObject.transform;

            //make sure rigidbody does not move while not driving
            rb.constraints = RigidbodyConstraints.FreezeAll;

            sitting = false;
        }
        
    }
    private void dismount(InputAction.CallbackContext context)
    {
        Debug.Log("DISMOUNT");
        if (sitting)
        {
            Sit();
        }
    }
}
