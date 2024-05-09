using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class handAnimator : MonoBehaviour
{
    [SerializeField] private InputActionProperty TriggerAction;
    [SerializeField] private InputActionProperty GripAction;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerVal = TriggerAction.action.ReadValue<float>();
        float gripVal = GripAction.action.ReadValue<float>();
        /*
        Debug.Log("Values:");
        Debug.Log(triggerVal);
        Debug.Log(gripVal);
        Debug.Log("");
        */
        anim.SetFloat("Trigger", triggerVal);
        anim.SetFloat("Grip", gripVal);
    }
}
