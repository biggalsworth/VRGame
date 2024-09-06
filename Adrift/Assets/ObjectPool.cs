using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public float amount = 100f;
    //dictionary containing an object and if it is available
    private Dictionary<GameObject, bool> objects = new Dictionary<GameObject, bool>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject tempObj;

        while (objects.Count < amount)
        {
            tempObj = Instantiate(prefab, gameObject.transform);
            tempObj.SetActive(false);
            objects.Add(tempObj, true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetObject()
    {
        //need to get the items from the dictionary
        foreach (KeyValuePair<GameObject, bool> obj in objects)
        {
            if(obj.Key.activeSelf == false)
            {
                objects[obj.Key] = true;
            }
            //check the availability
            if (obj.Value == true)
            {
                //update availability
                objects[obj.Key] = false;
                return obj.Key;
            }
        }
        Debug.Log("No available objects");
        return null;
    }
}
