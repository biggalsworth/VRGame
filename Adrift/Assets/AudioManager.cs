using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer master;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.Save();
    }

    public void SetMasterVol(float newLevel)
    {
        master.SetFloat("MasterVol", newLevel);
        PlayerPrefs.SetFloat("MasterVol", newLevel);
        PlayerPrefs.Save();
    }
    public void SetSFX(float newLevel)
    {
        master.SetFloat("SFXVol", newLevel);
        PlayerPrefs.SetFloat("SFXVol", newLevel);
        PlayerPrefs.Save();

    }
    public void SetMusicVol(float newLevel)
    {
        master.SetFloat("MusicVol", newLevel);
        PlayerPrefs.SetFloat("MusicVol", newLevel);
        PlayerPrefs.Save();
    }
}
