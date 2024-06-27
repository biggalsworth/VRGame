using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStats : MonoBehaviour
{
    public int maxHealth;
    float health;




    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destruct();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }


}
