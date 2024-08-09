using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicDrive : MonoBehaviour
{
    public GameObject player;
    public GameObject locomotion;

    public GameObject ShipMesh;

    public Transform seatPos;
    public Transform dismountPos;

    public InputActionReference dismountButton;

    [HideInInspector]
    public bool sitting = false;

    [Header("Data Input")]
    public GameObject throttle;
    public GameObject rotation;
    public Slider throttleSlider;

    public Button Ascension;
    public Selectable Descension;

    private float rotationSpeed = 1.5f;

    private float currThrottle;
    private float currRotation;

    //private Bonuses bonuses;

    public TextMeshProUGUI speedTracker;
    [HideInInspector]
    public float currVelocity = 0.0f;

    private Rigidbody rb;


    [Header("Audio")]
    public List<AudioSource> engineSources = new List<AudioSource>();
    public AnimationCurve SoundCurve;
    private float vol;


    void Awake()
    {
        Keyframe[] keyframes = SoundCurve.keys;

        keyframes[0].value = 0.1f;
        keyframes[0].time = 0;

        keyframes[1].time = 7;
        keyframes[1].value = 1;

        SoundCurve.keys = keyframes;

        dismountButton.action.performed += dismount;

        sitting = false;

        rb = GetComponent<Rigidbody>();
        //Make the ship static to start;
        //rb.constraints = RigidbodyConstraints.FreezeAll;

        //attach the player to the ship
        //player.transform.parent = gameObject.transform;

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
        if (GetComponent<JumpDrive>().jumping == false)
        {
            if (gameObject.GetComponent<ShipStats>().shipHealth > 10)
            {
                if (sitting)
                {
                    currThrottle = throttleSlider.value;
                    currRotation = rotation.GetComponent<ShipMovment>().rot;

                    //if (Math.Abs(currThrottle) > 0.5f && Math.Abs(rb.velocity.magnitude) <= Math.Abs(currThrottle))
                    if (Math.Abs(currThrottle) > 0.5f)// && Math.Abs(rb.velocity.magnitude - currThrottle) > 0.5f)
                    {
                        //add forward direction based on seat orientation
                        //make sure to make the throttle increase with the ships allowed speed in ship stats
                        //*1000.0 to counteract the heavy mass
                        rb.AddForce(seatPos.transform.forward * (currThrottle + GetComponent<ShipStats>().speed) * 1000.0f, ForceMode.Force);
                    }


                    float rotDiff = Mathf.Abs(transform.eulerAngles.y - currRotation);

                    gameObject.transform.rotation = Quaternion.Slerp(seatPos.transform.rotation, Quaternion.Euler(0, currRotation, 0), Time.deltaTime * (rotationSpeed - Bonuses.instance.rotationBonus));

                }
                else
                {
                    rb.velocity = Vector3.zero;
                    gameObject.isStatic = true;
                }

                currVelocity = rb.velocity.magnitude;
                currVelocity = Mathf.Round(currVelocity * 100) / 100;
                speedTracker.text = currVelocity.ToString();


                float audioVolume = SoundCurve.Evaluate(Math.Abs(currThrottle));
                foreach (AudioSource audio in engineSources)
                {
                    audio.volume = audioVolume;
                }

            }
            else
            {
                foreach (AudioSource audio in engineSources)
                {
                    audio.volume = 0;
                }

                rb.velocity = Vector3.zero;
                gameObject.isStatic = true;
            }
        }
    }


    public void Sit()
    {
        rb = GetComponent<Rigidbody>();

        if (!sitting)
        {
            ShipMesh.SetActive(false);

            gameObject.isStatic = false;

            //engineAudioSource1.Play();
            //engineAudioSource2.Play();

            //player.transform.parent = null;

            locomotion.GetComponent<LocomotionSystem>().enabled = false;
            locomotion.GetComponent<ContinuousMoveProviderBase>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;


            player.transform.position = new Vector3(seatPos.position.x, seatPos.position.y, seatPos.position.z);
            player.transform.rotation = Quaternion.Euler(0, seatPos.rotation.y, 0);

            player.transform.parent = gameObject.transform;
            //player.transform.localPosition = Vector3.zero;

            //make sure the rigidbody can move
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = Vector3.zero;

            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
            //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            //rb.constraints = RigidbodyConstraints.FreezePositionY;


            sitting = true;
        }
        
        else
        {
            ShipMesh.SetActive(true);

            player.transform.position = dismountPos.position;
            //player.transform.parent = gameObject.transform;
            player.transform.parent = null;

            locomotion.GetComponent<LocomotionSystem>().enabled = true;
            locomotion.GetComponent<ContinuousMoveProviderBase>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;



            gameObject.isStatic = true;
            rb.velocity = Vector3.zero;
            throttle.GetComponent<ShipMovment>().speed = 0f;

            //make sure rigidbody does not move while not driving
            rb.constraints = RigidbodyConstraints.FreezeAll;

            sitting = false;
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
