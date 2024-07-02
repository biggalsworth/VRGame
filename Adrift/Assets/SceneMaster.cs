using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);    
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void NextScene(string name)
    {
        if (dataManager != null)
        {
            dataManager.Save();
        }

        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void NextScene(int sceneId)
    {
        if (dataManager != null)
        {
            dataManager.Save();
        }

        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
       
    }
}
