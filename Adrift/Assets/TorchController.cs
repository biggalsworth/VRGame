using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject lightObj;

    // Start is called before the first frame update
    void Start()
    {
        lightObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Toggle(bool on)
    {
        if (on)
        {
            lightObj.SetActive(true);
        }
        else
        {
            lightObj.SetActive(false);
        }
    }
}
