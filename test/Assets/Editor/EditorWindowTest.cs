using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorWindowTest : EditorWindow {

    private string text;
    private Texture tex;
    [MenuItem("WindowTools/AddWindow")]
	static void AddWindow()
    {
        EditorWindowTest t = EditorWindow.GetWindow<EditorWindowTest>();
    }
    private void OnGUI()
    {
        text = EditorGUILayout.TextField("input text", text);
        if(GUILayout.Button("open notifycation",GUILayout.Width(200)))
        {
            this.ShowNotification(new GUIContent("this is a notification"));
        }
        if(GUILayout.Button("close notifycation",GUILayout.Width(200)))
            {
            this.RemoveNotification();
        }
        EditorGUILayout.LabelField("mouse Position", Event.current.mousePosition.ToString());
        tex = EditorGUILayout.ObjectField("Textture", tex, typeof(Texture), true) as Texture;

        if(GUILayout.Button("close window",GUILayout.Width(200)))
        {
            this.Close();
        }
    }
}
