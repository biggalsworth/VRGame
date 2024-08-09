using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum EnemyState
{
    wandering,
    engaging,
    idle,
    attack,
    dead
}

public class EnemyShipAI : MonoBehaviour
{
    EnemyShipStats stats;
    float health;

    Transform target;
    Vector3 targetFront;

    public float dist = 20f;

    EnemyState state = EnemyState.wandering;

    Rigidbody rb;
    public float speed = 10f;
    public float leapSpeed = 5f;
    public float maxWanderSpeed = 20f;

    public float detectionRange = 65f;

    bool frozen = false;

    private void Start()
    {
        stats = GetComponent<EnemyShipStats>();
        StartCoroutine(loop());
    }

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("ShipParent").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            // if ship is stunned, dont add any new force
            if (frozen)
            {
                continue;
            }

            #region Deciding state

            if (Vector3.Distance(target.position, transform.position) > dist + 5 && state != EnemyState.wandering)
            {
                state = EnemyState.engaging;
            }

            target = GameObject.Find("Ship").transform;
            targetFront = target.position + target.transform.forward * 15f;
            Vector3 playerDir = target.position - transform.position;
            // how far is the angle of the player relevant to our current sight
            float angleToPlayer = Vector3.Angle(transform.forward, playerDir);


            if (state == EnemyState.wandering)
            {
                if (rb.velocity.magnitude < maxWanderSpeed)
                {
                    rb.AddForce(transform.forward * speed, ForceMode.Force);
                }

                if (angleToPlayer < detectionRange && Vector3.Distance(target.position, gameObject.transform.position) < 90f)
                {
                    state = EnemyState.engaging;
                }

                /*
                if(angleToPlayer < 90 && Vector3.Distance(target.position, gameObject.transform.position) < 90f)
                {
                    state = EnemyState.engaging;
                }
                */
            }

            if (state == EnemyState.engaging)
            {
                // if we have shot too far past the player, this enemy needs to stop its current momentum
                if (Vector3.Distance(target.position, transform.position) > 90f && rb.velocity.magnitude > maxWanderSpeed + 5f)
                {
                    rb.velocity = Vector3.zero;
                }


                Debug.Log("ENGAGING");
                transform.LookAt(target.position);

                Vector3 playerSight = transform.position - target.position;

                // how far is the angle of the player relevant to our current sight
                float angleToEnemy = Vector3.Angle(target.transform.forward, playerSight);

                if (angleToEnemy > 80f)
                {
                    Debug.Log("Going to the front");
                    rb.velocity = Vector3.zero; rb.velocity.Normalize();
                    transform.LookAt(targetFront + target.transform.forward * 15f);
                    rb.AddForce(transform.forward * speed, ForceMode.Force);
                }   
                else
                {
                    Debug.Log("Moving");
                    if (Vector3.Distance(transform.position, target.position) > dist)
                    {
                        if (Random.Range(0, 5) == 0)
                        {
                            rb.AddForce(transform.forward * leapSpeed/2, ForceMode.Impulse);

                            switch (Random.Range(0, 5))
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
                                        rb.AddForce(transform.up * leapSpeed / 2, ForceMode.Impulse);
                                    }
                                    break;

                                case 2:
                                    if (target.position.y > transform.position.y)
                                    {
                                        rb.AddForce(-transform.up * leapSpeed / 2, ForceMode.Impulse);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            transform.LookAt(target.position);
                            if (rb.velocity.magnitude < maxWanderSpeed + 5)
                            {
                                rb.AddForce(transform.forward * speed, ForceMode.Force);
                            }
                        }

                    }
                    else
                    {
                        Debug.Log("Attempting to shoot");
                        if (rb.velocity.magnitude > 0)
                        {
                            rb.velocity = Vector3.zero;
                        }
                        state = EnemyState.attack;

                    }
                }
            }

            if (state == EnemyState.attack)
            {
                transform.LookAt(target.position);
                GetComponent<EnemyShipCombat>().Shoot();
                rb.AddForce(-transform.forward * speed/2, ForceMode.Impulse);
                state = EnemyState.engaging;

                Debug.Log("ENEMY FIRES");
            }

            #endregion

            #region Should We Board
             
            if(target.GetComponent<ShipStats>().shipHealth <= 10)
            {
                if(stats.health  > stats.maxHealth / 10)
                {
                    GameObject.Find("SceneManager").GetComponent<SceneMaster>().Invasion();
                    rb.velocity = Vector3.zero;
                    break;
                }   
                else
                {
                    transform.LookAt(target.position);
                    transform.Rotate(0, 180, 0);
                    rb.AddForce(transform.forward * (speed * 10f), ForceMode.Impulse);
                    yield return new WaitForSeconds(2f);
                    GetComponent<SpawnedObjClass>().Available();
                }
            }

            #endregion

        }

    }

    internal IEnumerator Freeze()
    {
        frozen = true;
        state = EnemyState.engaging;
        yield return new WaitForSeconds(5f);
        frozen = false;
    }
}
