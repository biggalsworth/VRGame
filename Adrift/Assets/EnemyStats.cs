using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float maxHealth = 100f;
    private float health;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Debug.Log("Die");
        }
    }
}
