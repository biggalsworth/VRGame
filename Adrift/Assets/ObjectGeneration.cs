using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public GameObject Ship;
    private BasicDrive shipStats;

    public List<GameObject> objects;

    // Start is called before the first frame update
    void Start()
    {
        shipStats = Ship.GetComponent<BasicDrive>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shipStats.currVelocity > 20.0f)
        {
            //Time.timeScale = 0.5f;
        }
    }
}
