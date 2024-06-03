using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{

    public GameObject Screen;
    public bool screenActive;


    GameObject currScreen;
    [Header("Radio")]
    public GameObject RadioPanel;
    public AudioSource soundControl;
    public List<AudioClip> music;
    public Slider volume;



    // Start is called before the first frame update
    void Start()
    {
        soundControl.clip = music[Random.Range(0, music.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        

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
