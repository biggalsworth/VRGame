using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ScareAnimations
{
    runAcross,
    Test
}

public class scareAnimation : MonoBehaviour
{
    public ScareAnimations animType;

    Animator anim;
    public GameObject monster;
    public Animator monsterAnim;

    //Running accross variables
    public Transform startPos;
    public Transform endPos;
    public float moveSpeed = 5f;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        monster = monsterAnim.gameObject;
        monster.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")

        {
            if(animType == ScareAnimations.runAcross)
            {
                //anim.Play("RunAcross");
                monster.transform.position = startPos.position;
                monster.transform.rotation = startPos.rotation;

                monster.SetActive(true);
                monsterAnim.SetBool("Run", true);
                moving = true;

            }
        }
    }

    private void CheckMovement()
    {
        if (moving)
        {
            var step = 5f * Time.deltaTime;
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, endPos.position, step);

            if (Vector3.Distance(monster.transform.position, endPos.position) < 0.01)
            {
                monster.SetActive(false);
                moving = false;
            }
        }
    }

}
