using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitConsole : MonoBehaviour
{
    public List<GameObject> screens;
    GameObject currScreen;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        currScreen = screens[0];
        currScreen.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapScreen(int screenID)
    {
        currScreen.SetActive(false);
        screens[screenID].SetActive(true);
        currScreen = screens[screenID];

    }
}
