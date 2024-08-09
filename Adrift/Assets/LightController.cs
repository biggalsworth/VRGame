using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Material lightmat;
    public Material normalMat;
    public Material dangerMat;

    public Color normalColour; 
    public Color DangerColour;

    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Danger()
    {
        light.color = DangerColour;
        light.gameObject.GetComponent<Animator>().Play("Flashing");
    }
    public void Normal()
    {
        light.color = normalColour;
        light.gameObject.GetComponent<Animator>().Play("Idle");

    }
}
