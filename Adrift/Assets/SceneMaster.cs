using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public DataManager dataManager;

    public bool canChange = true;

    [Header("For enemy boarding")]
    public GameObject WarningUI;


    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        WarningUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void NextScene(string name)
    {
        if (canChange)
        {
            canChange = false; // make sure multiple sources can't change the scene at once
            if (dataManager != null)
            {
                dataManager.Save();
            }

            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
    }

    public void NextScene(int sceneId)
    {
        if (canChange)
        {
            canChange = false; // make sure multiple sources can't change the scene at once
            if (dataManager != null)
            {
                dataManager.Save();
            }

            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }
    }


    public IEnumerator Invasion()
    {
        if (dataManager != null)
        {
            dataManager.Save();
        }
        WarningUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Invade", LoadSceneMode.Single);
    }

    public void ReturnToShip()
    {
        if (dataManager != null)
        {
            dataManager.Save();
        }
        SceneManager.LoadScene(PlayerPrefs.GetString("Scene"), LoadSceneMode.Single);
    }
}
