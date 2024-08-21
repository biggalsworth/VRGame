using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Transform target;

    NavMeshAgent agent;

    EnemyStats stats;

    [HideInInspector]
    public EnemyState state = EnemyState.wandering;

    public float detectionDistance = 50f;
    public float detectionAngle = 80;
    public float engageDistance = 15f;

    public float baseSpeed = 3f;

    float rotationSmooth = 50f;

    RaycastHit hit;

    public List<Transform> points = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PlayerCentre");
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();

        if (points.Count != 0)
        {
            target = points[Random.Range(0, points.Count)];
            agent.destination = target.position;
        }
        else
        {
            target = null;
            agent.destination = transform.position;
        }


        StartCoroutine(Loop());
    }

    // Update is called once per frame
    IEnumerator Loop()
    {
        while (true)
        {

            if (stats.health > 0)
            {

                Vector3 pos = transform.position + new Vector3(0, 2, 0);
                Debug.DrawRay(pos, transform.TransformDirection(transform.forward) * 100f, Color.green);

                //direction to the player
                Vector3 playerDir = (player.transform.position - transform.position).normalized;
                // how far is the angle of the player relevant to our current sight
                float angleToPlayer = Vector3.Angle(transform.forward, playerDir);


                if (state == EnemyState.wandering)
                {
                    if (angleToPlayer < detectionAngle && Vector3.Distance(player.transform.position, gameObject.transform.position) < detectionDistance)
                    {
                        if (Physics.Raycast(transform.position, playerDir, out hit, engageDistance))
                        {
                            if (hit.collider.tag == "Player")
                            {
                                state = EnemyState.engaging;
                            }
                        }
                    }
                    else
                    {
                        if (agent.remainingDistance < 1)
                        {
                            target = points[Random.Range(0, points.Count)];
                            agent.destination = target.position;
                        }
                    }

                }
                else
                {
                    agent.destination = player.transform.position;
                }

                if (state == EnemyState.engaging)
                {
                    if (agent.remainingDistance < engageDistance)
                    {
                        if (Physics.Raycast(transform.position, playerDir, out hit, engageDistance))
                        {
                            if (hit.collider.tag == "Player")
                            {
                                state = EnemyState.attack;
                            }
                        }
                    }
                }


                if (state == EnemyState.attack)
                {
                    agent.speed = 0;

                    if (agent.remainingDistance > engageDistance)
                    {
                        agent.speed = baseSpeed;

                        state = EnemyState.engaging;
                    }


                    if (agent.remainingDistance < engageDistance)
                    {
                        if (Physics.Raycast(transform.position, playerDir, out hit, engageDistance))
                        {
                            if (hit.collider.tag != "Player")
                            {
                                state = EnemyState.engaging;
                            }
                        }
                    }

                }
                else
                {
                    agent.speed = baseSpeed;
                }

            }
            else
            {
                state = EnemyState.dead;
                yield return new WaitForSeconds(2f);
                gameObject.SetActive(false);
            }


            yield return new WaitForSeconds(0.5f);

        }
    }

    private void Update()
    {
        if(state == EnemyState.attack)
        {
            Vector3 lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            rotation.x = 0;
            rotation.z = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSmooth * Time.deltaTime);
        }
    }
}
