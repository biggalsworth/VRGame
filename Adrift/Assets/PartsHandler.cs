using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsHandler : MonoBehaviour
{
    public List<GameObject> Parts;

    // Start is called before the first frame update
    void Start()
    {
        BreakParts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakParts()
    {
        //for(int i = 0; i < 3; i++)
        //{
        //    Parts[Random.Range(0, Parts.Count)].GetComponent<ShipPart>().Break();
        //}
        Parts[0].GetComponent<ShipPart>().Break();

    }
}
