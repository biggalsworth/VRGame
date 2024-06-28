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
        dataManager.Save();
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        if(dataManager != null)
        {
            dataManager.Save();
        }
    }

    public void NextScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        if (dataManager != null)
        {
            dataManager.Save();
        }
    }
}
