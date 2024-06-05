using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenType
{
    Stats,
    Weapons,
    Radio
}

public class ScreenManager : MonoBehaviour
{
    public GameObject Ship;
    private ShipStats stats;
    public GameObject Screen;
    public bool screenActive;


    GameObject currScreen;

    [Header ("Stats Screen")]
    public GameObject StatsPanel;

    public TextMeshProUGUI healthText;
    public RawImage healthIcon;
    public Color highHealthColour;
    public Color midHealthColour;
    public Color lowHealthColour;

    public TextMeshProUGUI inventoryAmount;
    public Slider inventorySlider;

    [Header("Weapon Systems")]
    public GameObject weaponsPanel;

    [Header("Radio")]
    public GameObject RadioPanel;
    public AudioSource soundControl;
    public List<AudioClip> music;
    public Slider volume;



    // Start is called before the first frame update
    void Start()
    {
        stats = Ship.GetComponent<ShipStats>();

        //stat setup
        inventorySlider.maxValue = stats.maxStorage;
        healthIcon.color = highHealthColour;

        soundControl.clip = music[Random.Range(0, music.Count)];

        currScreen = StatsPanel;
        Screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region Stats Update

        //health
        healthText.text = stats.shipHealth + "/" + stats.maxHealth;
        if(stats.shipHealth <= 25)
        {
            healthIcon.color = lowHealthColour;
        }
        else if(stats.shipHealth <= 75)
        {
            healthIcon.color = midHealthColour;
        }
        else if(stats.shipHealth > 75)
        {
            healthIcon.color = highHealthColour;
        }

        //inventory
        inventorySlider.value = stats.shipStorage;
        inventoryAmount.text = stats.shipStorage + "/" + stats.maxStorage;

        #endregion

        //Radio update
        soundControl.volume = volume.value;
    }


    //Main ScreenFunctions
    public void toggleScreen()
    {
        if(screenActive) 
        {
            Screen.SetActive(false);
            screenActive = false;
        }
        else
        {
            screenActive = true;
            Screen.SetActive(true);
        }
    }

    public void switchScreen(int screenId)
    {
        if(screenId == (int)ScreenType.Stats)
        {
            if(currScreen != StatsPanel)
            {
                Debug.Log("ShowStats");
                currScreen.SetActive(false);
                currScreen = StatsPanel;
                StatsPanel.SetActive(true);
            }
        }
        if(screenId == (int)ScreenType.Weapons)
        {
            if(currScreen != weaponsPanel)
            {
                Debug.Log("ShowWeapons");
                currScreen.SetActive(false);
                currScreen = weaponsPanel;
                weaponsPanel.SetActive(true);
            }
        }
        if(screenId == (int)ScreenType.Radio)
        {
            if(currScreen != RadioPanel)
            {
                Debug.Log("ShowRadio");
                currScreen.SetActive(false);
                currScreen = RadioPanel;
                RadioPanel.SetActive(true);
            }
        }
    }


    //Radio Functions

    public void PlayToggle()
    {
        if (soundControl.isPlaying)
        {
            soundControl.Pause();
        }
        else
        {
            soundControl.Play();

        }
    }

    public void skipSong()
    {
        soundControl.Stop();
        soundControl.clip = music[Random.Range(0, music.Count)];
        soundControl.Play();
    }
}
