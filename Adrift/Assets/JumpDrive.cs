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
    public TextMeshProUGUI SpeedValueText;

    BasicDrive driveScript;

    [HideInInspector]
    public bool jumping = false;

    public string sceneToLoad = "";

    public List<AudioSource> engineSources = new List<AudioSource>();
    public AudioClip JumpSound;

    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping)
        {
            gameObject.isStatic = true;
            SpeedValueText.text = "5.675 Mil";

        }
    }

    public void StartJump()
    {
        if(jumping == false)
        {
            if (sceneToLoad == "")
            {
                Debug.Log(sceneToLoad);
                warningTextBox.text = "No destination has been chosen";
            }
            else
            {
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneToLoad))
                {
                    warningTextBox.text = "Already in this system";
                }
                else
                {
                    StartCoroutine(Jump());
                }
            }
        }
    }

    IEnumerator Jump()
    {
        jumping = true;
        spawner.SetActive(false);
        gameObject.isStatic = true;
        foreach (AudioSource source in engineSources)
        {
            source.volume = 1;
            source.clip = JumpSound;
            source.Play();
        }
        yield return new WaitForSeconds(1f);
        portalAnim.Play("open");
        yield return new WaitForSeconds(5f);
        sceneLoader.NextScene(sceneToLoad);
    }

    public void SelectLocation(string name)
    {
        Debug.Log("Selected");
        sceneToLoad = name;
        Debug.Log("Travel to " + sceneToLoad);
    }
}
