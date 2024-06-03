using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Tracking.Collision.Active.Operation.Extraction;

public class SceneSetup : MonoBehaviour
{
    public Material LevelSkybox;

    // Start is called before the first frame update
    void Start()
    {
        if (LevelSkybox != null)
        {
            RenderSettings.skybox = LevelSkybox;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
