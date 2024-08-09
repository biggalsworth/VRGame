using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public Transform spawnPos;
    public ParticleSystem spawnEffect;
    public AudioSource soundSource;

    public GameObject[] food;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFood(int foodId)
    {
        spawnEffect.Play();
        soundSource.PlayOneShot(soundSource.clip);
        Instantiate(food[foodId], spawnPos.transform.position, Quaternion.identity, null);
    }
}
