using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    public Transform ship;
    public GameObject enemyIcon;

    Vector3 newZero;

    List<GameObject> enemies;
    List<GameObject> enemyIcons = new List<GameObject>();

    bool canScan = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canScan)
        {
            ClearIcons();
            newZero = ship.position;
            Vector3 tempPos;
            GameObject tempEnemy;

            RaycastHit[] hit;
            hit = Physics.SphereCastAll(ship.transform.position, 70, Vector3.forward);
            foreach (RaycastHit scanned in hit)
            {
                if (scanned.transform.tag != "Enemy")
                {
                    continue;
                }
                else
                {
                    tempPos = (scanned.transform.position - newZero) * 0.05f;
                    tempEnemy = Instantiate(enemyIcon, tempPos, Quaternion.identity, gameObject.transform);
                    tempEnemy.transform.localPosition = tempPos;
                    enemyIcons.Add(tempEnemy);
                }
            }
            StartCoroutine(ScanDelay());
        }
    }

    void ClearIcons()
    {
        if (enemyIcons.Count > 0)
        {
            foreach (GameObject enemy in enemyIcons)
            {
                enemyIcons.Remove(enemy);
                Destroy(enemy);
            }
        }
    }



    private IEnumerator ScanDelay()
    {
        canScan = false;
        yield return new WaitForSeconds(3f);
        canScan = true;
    }
}
