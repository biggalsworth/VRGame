using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using static UnityEngine.Rendering.DebugUI;

[CustomEditor(typeof(scareAnimation))]
public class ScareAnimationEditor : Editor
{
    public VisualTreeAsset VisualTree;

    private PropertyField animType;

    private SerializedProperty animProperty;
    private VisualElement runAcrossContent;

    private void OnEnable()
    {
        animProperty = serializedObject.FindProperty("animType");

    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        //add UXML builder
        VisualTree.CloneTree(root);

        animType = root.Q<PropertyField>("TypeOfAnim");
        animType.RegisterCallback<ChangeEvent<ScareAnimations>>(OnAnimationChanged);

        runAcrossContent = root.Q<VisualElement>("RunAcrossData");

        CheckAnimationType();

        return root;
    }

    private void OnAnimationChanged(ChangeEvent<ScareAnimations> evt)
    {
        CheckAnimationType();
    }

    public void CheckAnimationType()
    {
        //runAcross is index 0 in scare animation types
        if(animProperty.enumValueIndex == 0)
        {
            runAcrossContent.style.display = DisplayStyle.Flex;
        }
        else
        {
            runAcrossContent.style.display = DisplayStyle.None;
        }
    }
}
