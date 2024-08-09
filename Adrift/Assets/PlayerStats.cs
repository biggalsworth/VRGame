using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public CharacterController controller;

    ShipStats ship;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        ship = GameObject.FindGameObjectWithTag("ShipParent").GetComponent<ShipStats>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Died());
        }
    }

    private IEnumerator Died()
    {
        Time.timeScale = 0.1f;
        ship.cargoValue -= 20f;
        if(ship.cargoValue < 0)
        {
            ship.cargoValue = 0;
        }

        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1.0f;
        ship.shipHealth = 20;
        Bonuses.instance.currHealth = ship.shipHealth;
        Bonuses.instance.currValue = ship.cargoValue;
        GameObject.Find("SceneManager").GetComponent<SceneMaster>().ReturnToShip();

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            //hit.gameObject.GetComponent<BulletScript>().destroyBullet();
            //TakeDamage(10);
        }
    }

    internal void TakeDamage(float damage)
    {
        health -= damage;
    }
}
