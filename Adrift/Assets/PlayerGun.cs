using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGun : MonoBehaviour
{

    public GameObject bullet;
    public Transform shootPoint;
    public float shootCooldown = 0.5f;
    private bool canShoot = true;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (canShoot)
        {
            sound.PlayOneShot(sound.clip);
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            StartCoroutine(cooldown());
        }
    }


    IEnumerator cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot=true;
    }
}
