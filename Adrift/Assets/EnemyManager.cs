using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyManager : MonoBehaviour
{
    public int amount;
    int count;

    public List<GameObject> enemyPrefabs;
    private List<GameObject> enemies = new List<GameObject>();

    public EnemyBulletManager bulletController;


    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void CreateEnemies()
    {
        GameObject tempObj;

        while (enemies.Count < amount)
        {
            tempObj = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)], gameObject.transform);
            tempObj.GetComponent<SpawnedObjClass>().available = true;
            tempObj.GetComponent<EnemyShipCombat>().bulletSupply = bulletController;
            tempObj.SetActive(false);
            enemies.Add(tempObj);
        }
    }

    public GameObject GetEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject tempEnemy = enemies[i];
            if (tempEnemy.GetComponent<SpawnedObjClass>().available)
            {
                tempEnemy.GetComponent<EnemyShipStats>().health = tempEnemy.GetComponent<EnemyShipStats>().maxHealth;
                tempEnemy.GetComponent<SpawnedObjClass>().available = false;
                return tempEnemy;
            }
        }
        Debug.Log("No available enemies!");
        return null;
    }

    public int CheckActiveAmount()
    {
        int activeEnemyAmount = 0;
        foreach(GameObject tempEnemy in enemies)
        {
            if (tempEnemy.GetComponent<SpawnedObjClass>().available == false)
            {
                activeEnemyAmount++;
            }
        }

        return activeEnemyAmount;
    }
}
