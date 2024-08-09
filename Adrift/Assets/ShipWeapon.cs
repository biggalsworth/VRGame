using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ShipWeapon : MonoBehaviour
{
    [Header("Sound Management")]
    public AudioClip[] shootSounds;
    public AudioSource soundSource;

    [HideInInspector]
    public bool active;


    public float maxEnergy = 100;
    public float energy;

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public virtual void ResetLook()
    {
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    public void playSound()
    {
        soundSource.PlayOneShot( shootSounds[ Random.Range(0, shootSounds.Length - 1) ] );
    }

    public virtual void Shoot()
    {
        Debug.Log("EmptyShoot");
    }
    
    public IEnumerator RegenerateEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            energy += 5;
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }
    
}
