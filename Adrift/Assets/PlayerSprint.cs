using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
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
        UpdateInput();

        if(RightControllerVelocity.magnitude > 0.5f || LeftControllerVelocity.magnitude > 0.5f)
        {
            move.moveSpeed = 4;
        }
        else
        {
            move.moveSpeed = 2;
        }
    }

    void UpdateInput()
    {
        //LeftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
        //RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);

        //RightControllerVelocity = rightVelocity.ReadValue<Vector3>();
        //LeftControllerVelocity = leftVelocity.ReadValue<Vector3>();

        //RightControllerVelocity = rightVelocity.action.ReadValue<Vector3>();
        //LeftControllerVelocity = leftVelocity.action.ReadValue<Vector3>();
        
        Debug.Log(RightControllerVelocity.magnitude);
        Debug.Log(LeftControllerVelocity.magnitude);
    }
}
