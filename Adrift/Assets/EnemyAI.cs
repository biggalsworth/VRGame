using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Transform target;

    NavMeshAgent agent;

    EnemyStats stats;

    [HideInInspector]
    public EnemyState state = EnemyState.wandering;

    public float detectionRange = 80f;

    public List<Transform> points = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();


        target = points[Random.Range(0, points.Count)];
        agent.destination = target.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (stats.health > 0)
        {
            Vector3 playerDir = player.transform.position - transform.position;
            // how far is the angle of the player relevant to our current sight
            float angleToPlayer = Vector3.Angle(transform.forward, playerDir);


            if (state == EnemyState.wandering)
            {
                if (angleToPlayer < detectionRange && Vector3.Distance(player.transform.position, gameObject.transform.position) < 15f)
                {
                    state = EnemyState.engaging;
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
                if (agent.remainingDistance < 10f)
                {
                    state = EnemyState.attack;
                }
            }


            if (state == EnemyState.attack)
            {
                agent.speed = 0;

                Vector3 lookPos = player.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                rotation.x = 0;
                rotation.z = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.25f);

                if (agent.remainingDistance > 12f)
                {
                    agent.speed = 3.5f;

                    state = EnemyState.engaging;
                }
            }
            else
            {
                agent.speed = 3.5f;
            }
        }
        else
        {
            state = EnemyState.dead;
        }
    }
}
