using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerSprint : MonoBehaviour
{
    //InputDevice LeftControllerDevice;
    //InputDevice RightControllerDevice;
    Vector3 LeftControllerVelocity;
    Vector3 RightControllerVelocity;

    public InputActionReference leftVelocity;
    public InputActionReference rightVelocity;

    public ContinuousMoveProviderBase move;

    [Header("Footsteps")]
    public AudioSource source;
    public List<AudioClip> clips;
    CharacterController characterController;

    IEnumerator footsteps;


    private void Awake()
    {
        footsteps = FootstepLoop();

        characterController = GetComponent<CharacterController>();

        rightVelocity.action.performed += ctx => RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        leftVelocity.action.performed += ctx => LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();
    }

    private void OnEnable()
    {
        rightVelocity.action.performed -= ctx => RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        leftVelocity.action.performed -= ctx => LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();

        StartCoroutine(footsteps);
    }
    private void OnDisable()
    {
        rightVelocity.action.performed -= ctx => RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        leftVelocity.action.performed -= ctx => LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();

        StopCoroutine(footsteps);
    }

    void Update()
    {
        if(RightControllerVelocity.magnitude > 0.5f || LeftControllerVelocity.magnitude > 0.5f)
        {
            move.moveSpeed = 5;
        }
        else
        {
            move.moveSpeed = 3;
        }
    }


    IEnumerator FootstepLoop()
    {
        while (true)
        {
            if (characterController.velocity.magnitude > 0.5f)
            {
                source.PlayOneShot(clips[Random.Range(0, clips.Count)]);
            }

            if(move.moveSpeed == 5)
            {
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
