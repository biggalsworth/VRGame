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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveDamage(float damage)
    {
        if(bodyPart == bodyParts.Head)
        {
            damage += 5.0f;
        }

        transform.parent.GetComponent<EnemyStats>().TakeDamage(damage);

    }
}
