using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsHandler : MonoBehaviour
{
    public List<GameObject> Parts;

    // Start is called before the first frame update
    void Start()
    {
        //BreakPart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakPart()
    {
        for(int i = 0; i < Parts.Count; i++)
        {
            GameObject part = Parts[i];
            if (part.GetComponent<ShipPart>().currHealth >= part.GetComponent<ShipPart>().partHealth)
            {
                part.GetComponent<ShipPart>().Break();
                break;
            }
        }
    }
}
