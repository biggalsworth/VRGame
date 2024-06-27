using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(SpawnedObjClass))]
public class SpawnableObjUI : Editor
{
    public VisualTreeAsset visualTree;

    PropertyField valueField;
    PropertyField minVal;
    PropertyField maxVal;
    private PropertyField toggleProperty;
    private VisualElement randObjs;
    private VisualElement setObjs;
    private SerializedProperty hideProperty;

    private void OnEnable()
    {
        hideProperty = serializedObject.FindProperty("randomiseValue");
    }

    public override VisualElement CreateInspectorGUI()
    {

        VisualElement root = new VisualElement();

        visualTree.CloneTree(root);

        valueField = root.Q<PropertyField>("SetVal");
        minVal = root.Q<PropertyField>("minVal");
        maxVal = root.Q<PropertyField>("maxVal");

        toggleProperty = root.Q<PropertyField>("ToggleBool");
        toggleProperty.RegisterCallback<ChangeEvent<bool>>(OnBoolChanged);

        setObjs = root.Q<VisualElement>("SetVals");
        randObjs = root.Q<VisualElement>("RandomVals");


        CheckForDisplayType();

        //this would return it to normal as if it didnt have this script
        //return base.CreateInspectorGUI();

        return root;
    }


    private void OnBoolChanged(ChangeEvent<bool> evt)
    {
        CheckForDisplayType();

    }

    private void CheckForDisplayType()
    {
        if (hideProperty.boolValue)
        {
            randObjs.style.display = DisplayStyle.Flex;
            setObjs.style.display = DisplayStyle.None;
        }
        else
        {
            randObjs.style.display = DisplayStyle.None;
            setObjs.style.display = DisplayStyle.Flex;
        }
    }
}
