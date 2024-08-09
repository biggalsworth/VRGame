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
                creditsText.text = File.ReadAllText("Assets/Resources/Credits.txt");
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
        }
        else
        {
            Application.Quit();
        }
    }
}
