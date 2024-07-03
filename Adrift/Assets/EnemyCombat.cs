using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform shootPoint;

    public Animator gunAnim;

    EnemyAI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
        StartCoroutine(Loop());
    }

    // Update is called once per frame
    IEnumerator Loop()
    {
        while (true)
        {
            if (ai.state == EnemyState.attack)
            {
                shootPoint.LookAt(ai.player.transform.position);
                Instantiate(enemyBullet, shootPoint.transform.position, shootPoint.transform.rotation, null);
                gunAnim.SetBool("Shoot", true);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
