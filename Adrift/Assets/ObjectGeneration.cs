using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public GameObject Ship;
    private float spawnDist;
    private BasicDrive shipDrive;
    [HideInInspector]
    public bool canSpawn = true;

    [Header("Asteroids")]
    public AsteroidManager asteroidController; 
    [Range(0, 100)]
    public int asteroidProbability;

    [Header("Enemies")]
    public EnemyManager enemyController;
    [Range(0, 100)]
    public int enemyProbability;
    public int ActiveEnemyLimit = 5;
    bool SpawnEnemy = true;
    public float EnemySpawnCooldown = 20f;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy = true;
        shipDrive = Ship.GetComponent<BasicDrive>();
        spawnDist = Mathf.Abs(300);

    }

// Update is called once per frame
    void Update()
    {
        if (shipDrive.currVelocity > 1f && canSpawn)
        {
            if (Random.Range(0, 101) <= asteroidProbability)
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
            
            if (Random.Range(0, 101) <= enemyProbability && enemyController.CheckActiveAmount() < ActiveEnemyLimit && SpawnEnemy == true)
            {
                GameObject tempObj = enemyController.GetEnemy();
                if (tempObj != null)
                {
                    while (true)
                    {
                        Vector3 randPos = Vector3.zero;
                        if (Random.Range(0, 101) > 20)
                        {
                            randPos = Ship.transform.position + Ship.transform.forward * 200;
                            randPos = new Vector3(randPos.x + Random.Range(-10, 10), randPos.y + Random.Range(-10, 10), randPos.z + Random.Range(-10, 10));
                        }
                        //Have a small chance of spawning behind the player
                        else
                        {
                            randPos = Ship.transform.position + (-Ship.transform.forward * 200);
                            randPos = new Vector3(randPos.x + Random.Range(-10, 10), randPos.y + Random.Range(-10, 10), randPos.z + Random.Range(-10, 10));
                        }

                        //any other object in this location?
                        RaycastHit hit;
                        if (Physics.SphereCast(randPos, 20, transform.forward, out hit, 20))
                        {
                            //find a different location
                            continue;
                        }
                        else
                        {
                            tempObj.SetActive(true);
                            tempObj.transform.position = randPos;
                            tempObj.transform.LookAt(Ship.transform);
                            tempObj.transform.Rotate(0, Random.Range(0, 180), 0);
                            break;
                        }
                    }
                    StartCoroutine(EnemyDelay());
                }
            }
        }
    }

    IEnumerator EnemyDelay()
    {
        SpawnEnemy = false;
        yield return new WaitForSeconds(EnemySpawnCooldown);
        SpawnEnemy = true;
    }

}
