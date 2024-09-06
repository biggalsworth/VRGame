using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class MiningLaser : MonoBehaviour
{

    public Transform shootPoint;
    public float maxEnergy = 100;
    float energy;

    public LineRenderer laser;

    bool cooldown = false;
    bool shoot = false;
    bool recharging = true;

    GameObject currTarg;

    [Header("UI")]
    public TextMeshProUGUI laserInfo;

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;

        laser.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot && energy > 0)
        {
            LaserCalibrate();
            if (!cooldown)
            {
                StartCoroutine(Mine());
            }
        }
        if(!shoot || energy <= 0)
        {
            laser.gameObject.SetActive(false);
            if (energy < 0 && shoot == true)
            {
                BeginShoot();
            }
            if (recharging && energy < maxEnergy)
            {
                StartCoroutine(Recharge());
            }
        }

        laserInfo.text = energy + "/" + maxEnergy;
        
    }


    public void BeginShoot()
    {
        shoot = !shoot;
    }

    void LaserCalibrate()
    {
        laser.gameObject.SetActive(true);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 10f))
        {
            if (hit.collider.gameObject.tag == "SpawnedObject")
            {
                currTarg = hit.collider.gameObject;
            }
            else
            {
                currTarg = null;
            }

            laser.SetPosition(0, shootPoint.position);
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(0, shootPoint.position);
            laser.SetPosition(1, shootPoint.transform.position + shootPoint.forward * 10);
            currTarg = null;
        }
    }

    public IEnumerator Mine()
    {
        cooldown = true;

        energy -= 1;
        if (currTarg != null)
        {
            if (currTarg.GetComponent<SpawnedObjClass>().type == ObjectType.Asteroid)
            {
                Debug.Log("MINING ASTEROID");
                currTarg.GetComponent<SpawnedObjClass>().currDurability -= 5.0f + (Bonuses.instance.minerLevel * 1.5f);
            }
        }

        yield return new WaitForSeconds(1f);

        cooldown = false;
    }

    public IEnumerator Recharge()
    {
        recharging = false;
        energy += 1f;
        yield return new WaitForSeconds(0.5f);
        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }
        recharging = true;
    }
}
