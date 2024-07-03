using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnim : MonoBehaviour
{
    EnemyAI ai;

    public Animator anim;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.state == EnemyState.attack)
        {
            anim.SetBool("Aiming", true);
        }
        else
        {
            anim.SetBool("Aiming", false);
        }

        anim.SetFloat("Speed", agent.velocity.magnitude);

        if(ai.state == EnemyState.dead)
        {
            anim.SetBool("Dead", true);
            agent.isStopped = true;
        }
    }
}
