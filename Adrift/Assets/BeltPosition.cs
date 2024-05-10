using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPosition : MonoBehaviour
{
    public GameObject Camera;
    public float offset = 1.0f;
    float rotSpeed = 25f;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        //newPos = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float rotDiff = Mathf.Abs(Camera.transform.eulerAngles.y - transform.rotation.y);
        if(rotDiff >= 60)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0), rotSpeed * Time.deltaTime);
        }
        else if(rotDiff >= 40 && rotDiff < 60)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0), (rotSpeed / 2) * Time.deltaTime);
        }
        else if(rotDiff >= 20 && rotDiff < 40)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0), (rotSpeed / 4) * Time.deltaTime);
        }
        else if(rotDiff < 20)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0), (rotSpeed / 8) * Time.deltaTime);
        }
        
        newPos = new Vector3(Camera.transform.position.x, Camera.transform.position.y - offset, Camera.transform.position.z);

        transform.position = newPos;
    }
}
