using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    Bonuses bonuses;

    //Data Text
    string path = "Assets/Resources/Data/Progress.txt";

    string progressData;
    #region Progress Data Format String

    string progressFormat = String.Format(@"{0}: Engine
{1}: Stabilisers
{2}: Shield", 1, 1, 1);

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        bonuses = gameObject.GetComponent<Bonuses>();

        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        if (!File.Exists(path))
        {
            return;
        }
        string[] levels = File.ReadAllLines(path);
        string loadedlevels = "";
        foreach (string level in levels)
        {
            loadedlevels = loadedlevels+level[0];
        }
        bonuses.UpdateBonuses(loadedlevels);
    }

    [MenuItem("Tools/Save file")]
    public void Save()
    {

        progressFormat = String.Format(@"{0}: Engine
{1}: Stabilisers
{2}: Shield", bonuses.engineLevel, bonuses.stabiliserLevel, bonuses.shieldLevel);

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);

        //Create File if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Progress");
        }

        writer.Write(progressFormat);
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset currData = Resources.Load<TextAsset>(path);
        //Print the text from the file
        //Debug.Log(currData.text);
    }
}
