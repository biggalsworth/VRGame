using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    Bonuses data;

    //Data Text
    string path = "Assets/Resources/Data/Progress.txt";

    string progressData;
    #region Progress Data Format String

    string progressFormat = String.Format(@"{0}: Engine
{1}: Stabilisers
{2}: Shield
{3}: Storage
{4}: Value
{5}: Health
{6}: Wealth", 1, 1, 1, 0, 0, 100, 0);

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        data = gameObject.GetComponent<Bonuses>();

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
        List<string> loadedData = new List<string>();
        foreach (string level in levels)
        {
            string tempData = "";
            foreach(char c in level)
            {
                if (c != ':')
                {
                    tempData = tempData + c;
                }
                else
                {
                    loadedData.Add(tempData);
                    break;
                }
            }
        }
        data.UpdateBonuses(loadedData);
    }

    [MenuItem("Tools/Save file")]
    public void Save()
    {

        progressFormat = String.Format(@"{0}: Engine
{1}: Stabilisers
{2}: Shield
{3}: Storage
{4}: Value
{5}: Health
{6}: Wealth", data.engineLevel.ToString(), data.stabiliserLevel.ToString(), data.shieldLevel.ToString(), data.storageCount.ToString(), data.currValue.ToString(), data.currHealth.ToString(), data.currWealth.ToString());

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
