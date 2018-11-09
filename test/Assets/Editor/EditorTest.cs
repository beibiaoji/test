using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EditorTest1))]
public class EditorTest : Editor {

    public override void OnInspectorGUI()
    {
        EditorTest1 t = (EditorTest1)target;
        t.rect = EditorGUILayout.RectField("窗口坐標", t.rect);
        t.tex = EditorGUILayout.ObjectField("貼圖", t.tex, typeof(Texture), true) as Texture;
       if( GUILayout.Button("Build Object"))
        {

        }
    }
}
[CustomEditor(typeof(Camera))]
public class EditorTest2 : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("reset"))
        { }
    }
}
