using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        //get the current driving scene for the player to load back to
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
