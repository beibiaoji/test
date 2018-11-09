using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TestLoadAB : MonoBehaviour {

    string path; 
    private void Awake()
    {
        //Debug.Log(Application.streamingAssetsPath);
        //Debug.Log(Application.persistentDataPath);

        path = Application.dataPath + "/../../" + "AssetBundle";
        if (!Directory.Exists(path))
        {
          Debug.Log("AB 未sheng'cheng")
        }

    }
}
