using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public GameObject Ship;
    private float spawnDist;
    private BasicDrive shipStats;

    public AsteroidManager asteroidController; 
    [Range(0, 1)]
    public float asteroidProbability;

    public List<GameObject> objects;

    public bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        shipStats = Ship.GetComponent<BasicDrive>();
        spawnDist = Mathf.Abs(300);

    }

// Update is called once per frame
void Update()
    {
        if (shipStats.currVelocity > 1f && canSpawn)
        {
            if (Random.Range(0, 1) <= asteroidProbability)
            {
                GameObject tempObj = asteroidController.GetAsteroid();
                if (tempObj != null)
                {
                    while (true)
                    {
                        Vector3 randPos = new Vector3(Ship.transform.position.x + Random.Range(-spawnDist, spawnDist), Ship.transform.position.y + Random.Range(-spawnDist, spawnDist), Ship.transform.position.z + Random.Range(-spawnDist, spawnDist));
                        if (Vector3.Distance(Ship.transform.position, randPos) > 150)
                        {
                            RaycastHit hit;
                            if (Physics.SphereCast(randPos, 20, transform.forward, out hit, 20))
                            {
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
}
