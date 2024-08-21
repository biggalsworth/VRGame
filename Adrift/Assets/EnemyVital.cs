using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bodyParts
{
    Head,
    Body
}


public class EnemyVital : MonoBehaviour
{
    public bodyParts bodyPart;

    public EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void RecieveDamage(float damage)
    {
        if(bodyPart == bodyParts.Head)
        {
            damage += 5.0f;
        }

        stats.TakeDamage(damage);

    }
}
