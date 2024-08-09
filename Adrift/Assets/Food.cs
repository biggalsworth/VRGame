using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject mesh;
    public AudioSource source;

    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "MainCamera")
        {
            StartCoroutine(Eat());
        }
    }

    public IEnumerator Eat()
    {
        mesh.SetActive(false);
        source.PlayOneShot(sound);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
