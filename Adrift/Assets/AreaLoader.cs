using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    public List<GameObject> objectsToLoad;
    public List<GameObject> objectsToUnload;

    [Tooltip("Should this area be unloaded on start?")]
    public bool startDeloaded = true;

    bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (startDeloaded)
        {
            foreach (GameObject obj in objectsToUnload)
            {
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!loaded)
            {   
                LoadArea();
            }
        }
    }

    void LoadArea()
    {
        foreach (GameObject obj in objectsToLoad)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objectsToUnload)
        {
            obj.SetActive(false);
        }
    }



    private void Unload()
    {
        foreach (GameObject obj in objectsToUnload)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objectsToLoad)
        {
            obj.SetActive(false);
        }
    }
}
