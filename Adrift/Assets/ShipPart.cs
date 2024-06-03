using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    int partHealth = 10;
    public int currHealth = 0;

    [HideInInspector]
    public bool repairing = false;
    bool canHeal = true;

    public GameObject brokenMesh;
    public GameObject repairedMesh;

    // Start is called before the first frame update
    void Awake()
    {
        currHealth = partHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (canHeal && currHealth < partHealth)
        {
            StartCoroutine(Repair());
        }
        if (currHealth >= partHealth)
        {
            repairedMesh.SetActive(true);
            brokenMesh.SetActive(false);
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
