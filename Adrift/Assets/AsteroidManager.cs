using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;

    private List<GameObject> asteroids = new List<GameObject>();

    public int asteroidAmount = 100;

    //public List<GameObject> asteroids;

    // Start is called before the first frame update
    void Start()
    {
        while (asteroids.Count < asteroidAmount)
        {
            GameObject tempRoid = Instantiate(asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Count)], gameObject.transform);
            //tempRoid.AddComponent<SpawnedObjClass>();
            //tempRoid.GetComponent<SpawnedObjClass>().type = ObjectType.Asteroid;
            tempRoid.GetComponent<SpawnedObjClass>().available = true;
            tempRoid.SetActive(false);
            tempRoid.tag = "SpawnedObject";
            asteroids.Add(tempRoid);
        }

        DefaultAsteroids();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetAsteroid()
    {
        for(int i = 0; i < asteroids.Count; i++)
        {
            GameObject tempRoid = asteroids[i];
            if (tempRoid.GetComponent<SpawnedObjClass>().available)
            {
                //GameObject tempRoid = asteroids[index];
                asteroids.RemoveAt(i);
                asteroids.Add(tempRoid);

                tempRoid.GetComponent<SpawnedObjClass>().available = false;
                return tempRoid;
            }
        }
        return null;
    }

    private void DefaultAsteroids()
    {
        GameObject Ship = GameObject.Find("Ship");
        for (int i = 0; i < 40; i++)
        {
            GameObject tempObj = GetAsteroid();
            if (tempObj != null)
            {
                while (true)
                {
                    Vector3 randPos = new Vector3(Ship.transform.position.x + UnityEngine.Random.Range(-90, 90), Ship.transform.position.y + UnityEngine.Random.Range(-90, 90), Ship.transform.position.z + UnityEngine.Random.Range(-90, 90));
                    if (Vector3.Distance(Ship.transform.position, randPos) > 70)
                    {
                        RaycastHit hit;
                        if (Physics.SphereCast(randPos, 20, transform.forward, out hit, 20))
                        {
                            //spawned in another object;
                            continue;
                        }
                        else
                        {
                            tempObj.SetActive(true);
                            tempObj.transform.position = randPos;
                            break;
                        }

                    }
                }
            }
        }
    }
}
