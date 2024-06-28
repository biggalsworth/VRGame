using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpDrive : MonoBehaviour
{
    public Animator portalAnim;

    public SceneMaster sceneLoader;

    public GameObject spawner;

    public TextMeshProUGUI warningTextBox;

    BasicDrive driveScript;

    bool jumping = false;

    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartJump()
    {
        if(jumping == false)
        {
            jumping = true;
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        if (sceneToLoad == "")
        {
            warningTextBox.text = "No destination has been chosen";
            yield return new WaitForSeconds(0.01f);
        }
        else
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneToLoad))
            {
                warningTextBox.text = "Already in this system";
            }
            else
            {
                spawner.SetActive(false);
                gameObject.isStatic = true;
                portalAnim.Play("open");
                yield return new WaitForSeconds(5f);
                sceneLoader.NextScene(sceneToLoad);
            }
        }
    }

    public void SelectLocation(string name)
    {
        sceneToLoad = name;
    }
}
