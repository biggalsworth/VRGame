using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;

    GameObject activePanel;

    public GameObject MenuPanel;
    public GameObject ControlsPanel;

    public InputActionReference menuButton;
    bool menuActive = false;
    bool canPress = true;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        activePanel = MenuPanel;
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
    }
}
