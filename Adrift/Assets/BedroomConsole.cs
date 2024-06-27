using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomConsole : MonoBehaviour
{
    public List<Light> lights;

    public Color WakeUp;
    public Color Dusk;
    public Color Off;
    public Color zee;

    private int State = 0;

    private float currIntensity = 1;
    private float newIntensity = 1;
    private float oldIntensity = 1;

    private Color oldColor;
    private Color newColor;
    private Color currColor;

    private float timeElapsed = 0;
    private float shiftTime = 1f; //how long to shift between values

    private float colorTimeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currColor = WakeUp;
        newColor = WakeUp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currIntensity != newIntensity)
        {
            currIntensity = Mathf.Lerp(oldIntensity, newIntensity, timeElapsed / shiftTime);
            timeElapsed += Time.deltaTime;
        }

        if(currColor != newColor)
        {
            currColor = Color.Lerp(oldColor, newColor, colorTimeElapsed / shiftTime);
            colorTimeElapsed += Time.deltaTime;
        }

        foreach (Light light in lights)
        {
            light.intensity = currIntensity; 
            light.color = currColor;

        }
    }

    private void CheckLights()
    {
        switch (State)
        {
            case 0:
                ChangeLightColour(WakeUp, 1);
                break;

            case 1:
                ChangeLightColour(Dusk, 0.75f);
                break;

            case 2:
                ChangeLightColour(Off, 0f);
                break;
            case 3:
                ChangeLightColour(zee, 1f);
                break;

        }
    }

    private void ChangeLightColour(Color lightCol, float intensity)
    {
        timeElapsed = 0;
        oldIntensity = newIntensity;
        newIntensity = intensity;

        colorTimeElapsed = 0;
        oldColor = currColor;
        newColor = lightCol;
    }

    public void ChangeState(int newState)
    {
        if(State != newState)
        {
            State = newState;
            CheckLights();
        }
    }
}
