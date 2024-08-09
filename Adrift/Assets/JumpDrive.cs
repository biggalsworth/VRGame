using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpDrive : MonoBehaviour
{
    public Animator portalAnim;

    public SceneMaster sceneLoader;

    public GameObject spawner;

    public TextMeshProUGUI warningTextBox;
    public TextMeshProUGUI SpeedValueText;
    public TextMeshProUGUI destinationText;

    BasicDrive driveScript;
    ShipStats stats;

    [HideInInspector]
    public bool jumping = false;

    public string sceneToLoad = "";

    public List<AudioSource> engineSources = new List<AudioSource>();
    public AudioClip JumpSound;

    // Start is called before the first frame update
    void Start()
    {
        jumping = false;

        stats = GetComponent<ShipStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping)
        {
            gameObject.isStatic = true;
        }
    }

    public void StartJump()
    {
        if (jumping == false && stats.engaged == false)
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
        else if(stats.engaged == true)
        {
            warningTextBox.text = "Currently in combat";
        }
    }

    IEnumerator Jump()
    {
        if (stats.shipHealth > 10)
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
            yield return new WaitForSeconds(0.5f);
            SpeedValueText.text = "5.675 Mil";
            yield return new WaitForSeconds(5f);
            sceneLoader.NextScene(sceneToLoad);
        }
        else
        {
            warningTextBox.text = "Too Damaged";
        }
    }

    public void SelectLocation(string name)
    {
        int i = SceneManager.GetSceneByName(name).buildIndex;
        Debug.Log(i.ToString());
        if (name == "red")
        {
            destinationText.text = "Travel to\n Crimson System";
            sceneToLoad = SceneManager.GetSceneByBuildIndex(0).name;
        }
        else if(name == "station")
        {
            destinationText.text = "Travel to\n Epsilon Station";
            sceneToLoad = SceneManager.GetSceneByBuildIndex(1).name;


        }
        else if(name == "main")
        {
            destinationText.text = "Travel to\n Sminkoff Cluster";
            sceneToLoad = SceneManager.GetSceneByBuildIndex(2).name;


        }
        sceneToLoad = name;
        Debug.Log("Travel to " + sceneToLoad);
    }
}
