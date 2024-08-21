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


    private void Awake()
    {
        rightVelocity.action.performed += ctx => RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        leftVelocity.action.performed += ctx => LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();
    }

    private void OnEnable()
    {
        rightVelocity.action.performed -= ctx => RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        leftVelocity.action.performed -= ctx => LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();
    }
    private void OnDisable()
    {
        rightVelocity.action.performed -= ctx => RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        leftVelocity.action.performed -= ctx => LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();
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
}
