using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

enum EnemyState
{
    wandering,
    engaging,
    idle,
    attack
}

public class EnemyShipAI : MonoBehaviour
{
    EnemyShipStats stats;
    float health;

    Transform target;
    Vector3 targetFront;

    public float dist = 15f;

    EnemyState state = EnemyState.wandering;

    Rigidbody rb;
    public float speed = 10f;
    public float leapSpeed = 5f;
    public float maxWanderSpeed = 20f;

    public float detectionRange = 65f;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("ShipParent").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(target.position, transform.position) > dist && state != EnemyState.wandering)
        {
            state = EnemyState.engaging;
        }


        target = GameObject.Find("Ship").transform;
        targetFront = target.position + target.transform.forward * 20f;
        Vector3 playerDir = target.position - transform.position;
        // how far is the angle of the player relevant to our current sight
        float angleToPlayer = Vector3.Angle(transform.forward, playerDir);


        if (state == EnemyState.wandering)
        {
            if(rb.velocity.magnitude < maxWanderSpeed)
            {
                rb.AddForce(transform.forward * speed, ForceMode.Force);
            }

            if (angleToPlayer < detectionRange && Vector3.Distance(target.position, gameObject.transform.position) < 70f)
            {
                state = EnemyState.engaging;
            }
        }

        if(state == EnemyState.engaging)
        {
            Debug.Log("ENGAGING");
            RaycastHit hit;
            transform.LookAt(target.position);

            Vector3 playerSight = transform.position - target.position;

            // how far is the angle of the player relevant to our current sight
            float angleToEnemy = Vector3.Angle(target.transform.forward, playerSight);

            if (angleToEnemy > 25f)
            {
                Debug.Log("Going to the front");
                transform.LookAt(targetFront + target.transform.forward * 15f);
                if(rb.velocity.magnitude < maxWanderSpeed * 10)
                {
                    rb.AddForce(transform.forward * leapSpeed, ForceMode.Force);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, target.position) > dist)
                {
                    if (Random.Range(0, 5) == 0)
                    {
                        switch (Random.Range(0, 14))
                        {
                            case 0:
                                if (angleToPlayer < 0)
                                {
                                    rb.AddForce(transform.right * leapSpeed, ForceMode.Impulse);
                                }
                                else
                                {
                                    rb.AddForce(-transform.right * leapSpeed, ForceMode.Impulse);
                                }
                                break;

                            case 1:
                                if (target.position.y < transform.position.y)
                                {
                                    rb.AddForce(transform.up * leapSpeed/2, ForceMode.Impulse);
                                }
                                break;

                            case 2:
                                if (target.position.y > transform.position.y)
                                {
                                    rb.AddForce(-transform.up * leapSpeed/2, ForceMode.Impulse);
                                }
                                break;
                        }
                    }
                    else
                    {
                        transform.LookAt(target.position);
                        if (rb.velocity.magnitude < maxWanderSpeed)
                        {
                            rb.AddForce(transform.forward * speed, ForceMode.Force);
                        }
                    }

                }
                else
                {
                    if (rb.velocity.magnitude < maxWanderSpeed * 1.25f)
                    {
                        rb.velocity = Vector3.zero;
                    }
                    state = EnemyState.attack;
                    transform.LookAt(targetFront);

                }
            }
        }


        if(state == EnemyState.attack)
        {
            transform.LookAt(target.position);
            if (Random.Range(0, 20) == 0)
            {
                transform.LookAt(target.position);
                //rb.AddForce(-transform.forward * speed, ForceMode.Impulse);
            }
            else
            {
                GetComponent<EnemyShipCombat>().Shoot();
            }

            Debug.Log("ENEMY FIRES");
        }

    }
}
