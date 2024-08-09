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
        scene.ReturnToShip();
    }
}
