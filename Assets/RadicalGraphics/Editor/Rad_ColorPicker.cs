using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class Rad_ColorPicker : EditorWindow
{
    [SerializeField]
    private bool _usedColorPickerOnce = false;

    [SerializeField]
    private Color _color = Color.white;
    protected Color color
    {
        get
        {
            if (!_usedColorPickerOnce)
            {
                return Color.white;
            }

            return _color;
        }
        set
        {
            if (!_usedColorPickerOnce && value == Color.white)
            {
                return;
            }

            if (_usedColorPickerOnce && value == _color)
            {
                return;
            }

            _color = value;
            _usedColorPickerOnce = true;

            EditorGUIUtility.systemCopyBuffer = GetColorString();
        }
    }

    // ================================================================================
    //  creating the window
    // --------------------------------------------------------------------------------

    [MenuItem("Radical Graphics/Tools/Color To C#")]
    private static void LevelEditorWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(Rad_ColorPicker));
        window.titleContent = new GUIContent("Color to C#");
    }

    // ================================================================================
    //  unity methods
    // -------------------------------------------------------------------------------   

    private void OnGUI()
    {
        EditorGUILayout.Space();

        color = EditorGUILayout.ColorField("Color", color);

        EditorGUILayout.Space();

        string colorLabel;
        string alternativeColorLabel;
        if (!_usedColorPickerOnce)
        {
            GUI.enabled = false;
            colorLabel = "";
            alternativeColorLabel = "";
        } else
        {
            colorLabel = GetColorString();
            alternativeColorLabel = GetColor32String();
        }

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField(colorLabel);
        GUILayout.Space(5f);
        if (GUILayout.Button("Copy"))
        {
            EditorGUIUtility.systemCopyBuffer = GetColorString();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField(alternativeColorLabel);
        GUILayout.Space(5f);
        if (GUILayout.Button("Copy"))
        {
            EditorGUIUtility.systemCopyBuffer = GetColor32String();
        }
        GUILayout.EndHorizontal();

        GUI.enabled = true;

        EditorGUILayout.Space();

        if (_usedColorPickerOnce)
        {
            EditorGUILayout.LabelField("Copied to System Clipboard.");
        } else
        {
            EditorGUILayout.LabelField("Select a color.");
        }
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private string GetColorString()
    {
        return "new Color(" + color.r + "f," + color.g + "f," + color.b + "f," + color.a + "f)";
    }

    private string GetColor32String()
    {
        Color32 color32 = (Color32)color;
        return "new Color32(" + color32.r + "," + color32.g + "," + color32.b + "," + color32.a + ")";
    }
}