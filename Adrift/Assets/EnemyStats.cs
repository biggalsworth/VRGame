using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float maxHealth = 100f;
    public float health;

    bool dead = false;



    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        GameObject.Find("Enemies").GetComponent<InvasionManager>().enemyCount++;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(float damage)
    {
        if (!dead)
        {
            health -= damage;

            if (health <= 0)
            {
                dead = true;
                health = 0;
                GameObject.Find("Enemies").GetComponent<InvasionManager>().enemyCount--;
            }
        }
    }
}
