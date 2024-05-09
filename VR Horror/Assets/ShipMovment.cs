using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public enum DriveType
{
    Throttle,
    Rotate,
    Brake
}

public class ShipMovment : MonoBehaviour
{
    [Tooltip("is this for throttle or rotation")]
    public DriveType type;

    public Transform ParentAnchor;

    [HideInInspector]
    public float speed;
    public float rot;

    private float pos;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (type == DriveType.Brake)
        //{
        //    if(GetComponent<XRPokeFilter>().pokeStateData.Value)
        //}
        //transform.position = ParentAnchor.position;
        //GetComponent<HingeJoint>().connectedAnchor = ParentAnchor.position;
    }

    public void Reset()
    {
        speed = 0;
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        //GetComponent<HingeJoint>().axis = Vector3.zero;
    }

    public void ThrottleChange(float change)
    {
        if(speed+change < 5 && speed+change > -5)
        {
            speed += change;
        }
    }
    public void rotChange(float change)
    {
        rot += change;
    }
}
