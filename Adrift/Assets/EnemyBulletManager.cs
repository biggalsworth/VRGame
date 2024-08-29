using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float amount = 200f;
    private List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject tempObj;

        while (bullets.Count < amount)
        {
            tempObj = Instantiate(bulletPrefab, gameObject.transform);
            tempObj.SetActive(false);
            tempObj.GetComponent<EnemyShipBullet>().available = true;
            bullets.Add(tempObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        foreach (GameObject obj in bullets)
        {
            if(obj.GetComponent<EnemyShipBullet>().available == true)
            {
                obj.GetComponent<EnemyShipBullet>().available = false;
                //obj.SetActive(true);
                return obj;
            }
        }
        Debug.Log("No available bullets");
        return null;
    }
}
