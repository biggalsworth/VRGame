using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerGun : MonoBehaviour
{

    public GameObject bullet;
    public Transform shootPoint;
    public float shootCooldown = 0.5f;
    private bool canShoot = true;

    public int maxEnergy = 100;
    public int energy;
    public int shotCost = 5;

    public Material HighEnergy;
    public Material MidEnergy;
    public Material LowEnergy;
    public Image energyIcon;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;

        sound = GetComponent<AudioSource>();

        energy = maxEnergy;

        StartCoroutine(energyRegen());
    }

    // Update is called once per frame
    void Update()
    {
        if (energy <= 20)
        {
            energyIcon.material = LowEnergy;
        }
        else if (energy <= maxEnergy / 2)
        {
            energyIcon.material = MidEnergy;
        }
        else if (energy > maxEnergy / 2)
        {
            energyIcon.material = HighEnergy;
        }
    }

    public void Shoot()
    {
        if (canShoot && energy > shotCost)
        {
            energy -= shotCost;
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
    IEnumerator energyRegen()
    {
        while (true)
        {
            if(energy + shotCost >= maxEnergy)
            {
                energy = maxEnergy;
            }
            else
            {
                energy += shotCost;
            }
            yield return new WaitForSeconds(1f);
        }
    }


    [Tooltip ("Should this object be a collider or trigger.\nTrue for solid collider, false for trigger")]
    public void ShouldCollide(bool colliding)
    {
        GetComponent<Collider>().isTrigger = !colliding;
    }
}
