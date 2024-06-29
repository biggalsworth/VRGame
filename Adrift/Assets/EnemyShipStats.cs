using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStats : MonoBehaviour
{
    public int maxHealth;
    float health;

    [HideInInspector]
    public bool dead = false;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(Destruct());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public IEnumerator Destruct()
    {
        GetComponent<SpawnedObjClass>().durability = 0f;
        GetComponent<Animator>().Play("Destroy");
        yield return new WaitForSeconds(2f);
        GetComponent<SpawnedObjClass>().Available();

    }


}
