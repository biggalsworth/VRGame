using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareMaster : MonoBehaviour
{

    public AudioClip ambience;
    public AudioClip monsters;

    public AudioSource soundSource;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scareLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator scareLoop()
    {
        while (true)
        {
            int chance = Random.Range(0, 10);
            if (chance < 3)
            {
                //play an ambience sound
                if( chance % 2 == 0)
                {
                    soundSource.gameObject.transform.position = new Vector3(player.transform.position.x + Random.Range(20, 50), player.transform.position.y, player.transform.position.z + Random.Range(20, 50));
                }
                else
                {
                    soundSource.gameObject.transform.position = new Vector3(player.transform.position.x - Random.Range(20, 50), player.transform.position.y, player.transform.position.z - Random.Range(20, 50));
                }

                soundSource.PlayOneShot(monsters);

            }

            yield return new WaitForSeconds(30f);
        }
    }
}
