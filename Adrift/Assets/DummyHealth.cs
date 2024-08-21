using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DummyHealth : EnemyVital
{
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void RecieveDamage(float damage)
    {
        if (bodyPart == bodyParts.Head)
        {
            damage += 5;
        }

        StartCoroutine(DamageShow(damage));
    }


    IEnumerator DamageShow(float damage)
    {
        text.gameObject.SetActive(true);
        text.text = damage.ToString();
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
    }
}
