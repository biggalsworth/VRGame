using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;

    private List<GameObject> asteroids = new List<GameObject>();

    public int asteroidAmount = 50;

    //public List<GameObject> asteroids;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(asteroidPrefabs.Count - 1);
        while (asteroids.Count < asteroidAmount)
        {
            GameObject tempRoid = Instantiate(asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Count)], gameObject.transform);
            tempRoid.AddComponent<AsteroidClass>();
            tempRoid.SetActive(false);
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
            if (tempRoid.GetComponent<AsteroidClass>().available)
            {
                //GameObject tempRoid = asteroids[index];
                asteroids.RemoveAt(i);
                asteroids.Add(tempRoid);

                tempRoid.SetActive(true);
                tempRoid.GetComponent<AsteroidClass>().available = false;
                return tempRoid;
            }
        }
        return null;
    }

    private void DefaultAsteroids()
    {
        GameObject Ship = GameObject.Find("Ship");
        for (int i = 0; i < 20; i++)
        {
            GameObject tempObj = GetAsteroid();
            if (tempObj != null)
            {
                Vector3 randPos = new Vector3(Ship.transform.position.x + UnityEngine.Random.Range(-50, 50), Ship.transform.position.y + UnityEngine.Random.Range(-50, 50), Ship.transform.position.z + UnityEngine.Random.Range(-50, 50));
                if (Vector3.Distance(Ship.transform.position, randPos) > 50)
                {
                    RaycastHit hit;
                    if (Physics.SphereCast(randPos, 20, transform.forward, out hit, 20))
                    {
                        //spawned in another object;
                        return;
                    }
                    else
                    {
                        tempObj.transform.position = randPos;
                    }

                }

            }
        }
    }
}
