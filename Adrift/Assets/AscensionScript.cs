using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AscensionScript : MonoBehaviour
{
    public Rigidbody ship;

    public Slider slider;

    public float speed = 2f;

    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 0)
        {
            Vector3 tempVel = ship.velocity;
            tempVel.y = 0;
            ship.velocity = tempVel;
        }
        else
        {
            //if (active == true)
            //{
                ship.AddForce(ship.gameObject.transform.up * (slider.value * 1000.0f * speed), ForceMode.Force);
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            active = true;
            Debug.Log("ASCEND");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            active = false;

            Vector3 tempVel = ship.velocity;
            tempVel.y = 0;
            ship.velocity = tempVel;
        }
    }
}
