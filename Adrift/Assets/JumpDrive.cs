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

    //public string sceneToLoad = "";
    public int sceneToLoad = -1;

    public List<AudioSource> engineSources = new List<AudioSource>();
    public AudioClip JumpSound;

    // Start is called before the first frame update
    void Start()
    {
        warningTextBox.text = "";

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
            if (sceneToLoad == -1)
            {
                Debug.Log(sceneToLoad);
                warningTextBox.text = "No destination has been chosen";
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == sceneToLoad)
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

    public void SelectLocation(int id)
    {
        //int i = SceneManager.GetSceneByName(name).buildIndex;
        warningTextBox.text = "";

        Debug.Log(id);
        if (id == 0)
        {
            destinationText.text = "Travel to\n Crimson System";
            sceneToLoad = 0;
            Debug.Log(SceneManager.GetSceneByBuildIndex(0).name);

        }
        if (id == 1)
        {
            destinationText.text = "Travel to\n Epsilon Station";
            sceneToLoad = 1;

        }
        if(id == 2)
        {
            destinationText.text = "Travel to\n Sminkoff Cluster";
            sceneToLoad = 2;
            Debug.Log(SceneManager.GetSceneByBuildIndex(2).name);

        }
        if(id == 3)
        {
            destinationText.text = "Travel to\n Atruvis's Graveyard";

            sceneToLoad = 5;

            Debug.Log(SceneManager.GetSceneByBuildIndex(5).name);

            Debug.Log(SceneManager.GetSceneByBuildIndex(sceneToLoad).name);

        }
        Debug.Log("Travel to " + sceneToLoad);
    }
}
