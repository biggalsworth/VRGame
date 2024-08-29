using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabGunPose : MonoBehaviour
{
    Animator RHand;
    Animator LHand;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabber = GetComponent<XRGrabInteractable>();

        grabber.selectEntered.AddListener(SetupPose);
        grabber.selectExited.AddListener(ResetPose);

    }


    public void SetupPose(BaseInteractionEventArgs args)
    {
        Debug.Log(args.interactorObject.transform.name);
        try
        {
            args.interactorObject.transform.GetComponentInChildren<Animator>().SetBool("Gun", true);
        }
        catch { }
    }
    public void ResetPose(BaseInteractionEventArgs args)
    {
        try
        {
            args.interactorObject.transform.GetComponentInChildren<Animator>().SetBool("Gun", false);
        }
        catch { }
    }
}
