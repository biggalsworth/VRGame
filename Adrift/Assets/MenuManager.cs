using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;

    GameObject activePanel;

    public GameObject MenuPanel;
    public GameObject ControlsPanel;
    public GameObject audioPanel;
    public GameObject creditsPanel;
    public GameObject quitPanel;

    public TextMeshProUGUI creditsText;

    public InputActionReference menuButton;
    bool menuActive = false;
    bool canPress = true;

    // Start is called before the first frame update
    void Start()
    {
        quitPanel.SetActive(true);
        activePanel = quitPanel;
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButton.action.triggered == true)
        {
            toggleMenu();
        }
    }


    void toggleMenu()
    {
        if (menuActive == false)
        {
            menuActive = true;
            menuCanvas.SetActive(true);
        }
        else
        {
            menuActive = false;
            menuCanvas.SetActive(false);
        }
    }

    public void ChangeScreen(int screenID)
    {
        if(screenID == 0)
        {
            if(activePanel != MenuPanel)
            {
                activePanel.SetActive(false);
                MenuPanel.SetActive(true);
                activePanel = MenuPanel;
            }
        }
        if(screenID == 1)
        {
            if(activePanel != ControlsPanel)
            {
                activePanel.SetActive(false);
                ControlsPanel.SetActive(true);
                activePanel = ControlsPanel;
            }
        }
        if(screenID == 2)
        {
            if(activePanel != audioPanel)
            {
                activePanel.SetActive(false);
                audioPanel.SetActive(true);
                activePanel = audioPanel;
            }
        }
        if(screenID == 5)
        {
            if(activePanel != creditsPanel)
            {
                activePanel.SetActive(false);
                creditsPanel.SetActive(true);
                activePanel = creditsPanel;
                creditsText.text = @"
Unity Asset Store Assets:

Sci-Fi Styled Modular Pack by KARBOOSX
Deep Space Skybox Pack by SEAN DUFFY
Breakable Asteroids by CHADDERBOX
It's a Pipe Dream Kitbash Pack by ONE POTATO KINGDOM STUDIO
Laser Turret by URSA.ANIM
Oxar Light Freighter by FIREBOLT STUDIOS
QA Book by QATMO
Sci-Fi PBR props by NEUTRONCAT
Sci Fi Space Soldier PolygonR by POLYGONR
Sci-Fi HandGun by LIFE HACKER
Alien Ships Pack by AUTARCA
Kitchen Appliance - Low Poly by ALSTRA INFINITE
Food Pack Mixed by BRAIN FAIL PRODUCTIONS
Sci-fi GUI skin by 3D.RINA
Sci Fi Gun by MASH VIRTUAL
Ghoul Anklegrabber by SKULLVERTEX

Audio 
Audio and some music provided by Pixabay
Thanks to KSI for making dissimulation copyright free!

Programming
Benjamin Jukes
";
            }
        }
        if(screenID == 6)
        {
            if(activePanel != quitPanel)
            {
                activePanel.SetActive(false);
                quitPanel.SetActive(true);
                activePanel = quitPanel;
            }
        }
    }



    public void Quit(bool save)
    {
        if (save)
        {
            DataManager.instance.Save();
            Application.Quit();
        }
        else
        {
            Application.Quit();
        }
    }
}
