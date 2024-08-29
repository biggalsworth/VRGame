using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    public int partHealth = 10;
    [HideInInspector]
    public int currHealth = 0;

    [HideInInspector]
    bool broken;
    public bool repairing = false;
    bool canHeal = true;

    public GameObject brokenMesh;
    public GameObject repairedMesh;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = partHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (canHeal && currHealth < partHealth)
        {
            StartCoroutine(Repair());
            broken = true;
        }
        if (currHealth >= partHealth && broken)
        {
            brokenMesh.SetActive(false);
            repairedMesh.SetActive(true);
            broken = false;
            Debug.Log("REPAIRED");
            GameObject.FindGameObjectWithTag("ShipParent").GetComponent<ShipStats>().shipHealth += 10f;
            GameObject.FindGameObjectWithTag("ShipParent").GetComponent<ShipStats>().damageSeverity -= 1;
        }

        if(currHealth >= partHealth && !repairedMesh.activeSelf)
        {
            brokenMesh.SetActive(false);
            repairedMesh.SetActive(true);
        }
    }

    IEnumerator Repair()
    {
        canHeal = false;
        if (repairing && currHealth < partHealth)
        {
            Debug.Log("Healed");
            currHealth += 1;
        }

        yield return new WaitForSeconds(0.1f);
        canHeal = true;
    }

    public void Break()
    {
        currHealth = 0;
        repairedMesh.SetActive(false);
        brokenMesh.SetActive(true);
    }
    

}
