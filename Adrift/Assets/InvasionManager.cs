using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvasionManager : MonoBehaviour
{
    [HideInInspector]
    public int enemyCount;

    private SceneMaster scene;

    public GameObject CompletedUI;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("SceneManager").GetComponent<SceneMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount == 0)
        {
            InvasionConquered();
        }
    }

    private void InvasionConquered()
    {
        StartCoroutine(EndInvasion());
    }

    IEnumerator EndInvasion()
    {
        CompletedUI.SetActive(true);

        yield return new WaitForSeconds(5f);

        scene.ReturnToShip();

    }
}
