using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    private Animator anim;
    AudioSource audio;

    public bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player" || other.tag == "Enemy") && !locked)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Enemy") && !locked)
        {
            anim.Play("close");
        }
    }
}
