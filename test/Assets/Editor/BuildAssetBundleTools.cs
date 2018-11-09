using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class BuildAssetBundleTools : Editor
{
   public static List<AssetBundleBuild> abs = new List<AssetBundleBuild>();

    [MenuItem("Tools/BulidAB")]
    public static void BuildAB()
    {
        string path = Application.dataPath+"/../../" + "AssetBundle";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        BuildPipeline.BuildAssetBundles(path,abs.ToArray(), BuildAssetBundleOptions.None
            , BuildTarget.StandaloneWindows64);
    }
    [MenuItem("Tools/AddABName")]
    public static void AddABName()
    {
        string path = Application.dataPath + "/Download";
        List<string> allfile = new List<string>();
        Director(path, allfile);
        for(int i=allfile.Count-1;i>=0;i--)
        {
            if(Path.GetExtension(allfile[i])==".meta")
            {
                allfile.RemoveAt(i);
            }
            else
            {
                allfile[i] = allfile[i].Replace("\\", "/");
                allfile[i] = allfile[i].Substring(allfile[i].IndexOf("Assets/Download/"));
            }
        }
        string[] paths = AssetDatabase.GetDependencies(allfile.ToArray());
        for(int i=0;i<paths.Length;i++)
        {
            if(!allfile.Contains(paths[i]))
            {
                allfile.Add(paths[i]);
            }
        }
        for(int i=allfile.Count-1;i>=0;i--)
        {
            if (allfile[i].IndexOf(".cs") > -1 || allfile[i].IndexOf(".DS_Store") > -1 || allfile[i].IndexOf(".svn") > -1 || allfile[i].IndexOf(".tpsheet") > -1)
            {
                allfile.RemoveAt(i);
            }
        }
        var allComputeBundleDic = ComputeBundleDir(allfile);
       
        var allComputeBundleDicInfo = allComputeBundleDic.GetEnumerator();
        while (allComputeBundleDicInfo.MoveNext())
        {
            var k = allComputeBundleDicInfo.Current.Key;
            var v = allComputeBundleDicInfo.Current.Value;
            var absSub = new AssetBundleBuild();
            absSub.assetNames = v.ToArray();
            absSub.assetBundleName = k.ToLower();
            AssetImporter importer = AssetImporter.GetAtPath(k);
            if(importer!=null)
            {
                importer.assetBundleName = k;
                importer.assetBundleVariant = "assetbundle";
            }
            abs.Add(absSub);
        }
        allComputeBundleDicInfo.Dispose();
    }

    static Dictionary<string, List<string>> ComputeBundleDir(List<string> all)
    {
        Dictionary<string, List<string>> allDic = new Dictionary<string, List<string>>();
        for (int i = 0; i < all.Count; i++)
        {
            string p = "";
            if (all[i].IndexOf(".") > -1)
            {
                p = all[i].Replace("/" + Path.GetFileName(all[i]), "");
            }
            else
            {
                p = all[i];
            }
            var s = "";
            if (p.IndexOf("Assets/Download/") > -1)
            {
                s = p.Substring(p.IndexOf("Assets/Download/") + "Assets/Download/".Length);
            }
            else
            {
                s = p;
            }
            if (all[i].IndexOf(".cs") > -1 || all[i].IndexOf(".DS_Store") > -1 || all[i].IndexOf(".svn") > -1 || all[i].IndexOf(".tpsheet") > -1)
            {
                continue;
            }
            if (all[i].IndexOf("Assets/Download") > -1 )
            {
                if (all[i].IndexOf("Assets/Download") > -1)
                {
                    if (!allDic.ContainsKey(s + "/" + Path.GetFileNameWithoutExtension(all[i])))
                    {
                        allDic.Add(s + "/" + Path.GetFileNameWithoutExtension(all[i]), new List<string>() { all[i] });
                    }
                    else
                    {
                        Debug.LogError("重复的key:" + s + "/" + Path.GetFileNameWithoutExtension(all[i]));
                    }
                }
                else
                {
                    if (!allDic.ContainsKey(s + "/" + Path.GetFileNameWithoutExtension(all[i])))
                    {
                        allDic.Add(s + "/" + Path.GetFileName(all[i]), new List<string>() { all[i] });
                    }
                    else
                    {
                        Debug.LogError("重复的key:" + s + "/" + Path.GetFileNameWithoutExtension(all[i]));
                    }
                }

            }
            else
            {
                if (allDic.ContainsKey(s))
                {
                    allDic[s].Add(all[i]);
                }
                else
                {
                    allDic.Add(s, new List<string>() { all[i] });
                }
            }
        }
        return allDic;
    }
    public static void Director(string dir, List<string> fileList)
    {
        if (!Directory.Exists(dir))
        {
            return;
        }
        DirectoryInfo d = new DirectoryInfo(dir);
        if (d == null)
            return;
        FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
        foreach (FileSystemInfo fsinfo in fsinfos)
        {
            if (fsinfo is DirectoryInfo)
            {
                Director(fsinfo.FullName, fileList);
            }
            else
            {
                fileList.Add(fsinfo.FullName);
            }
        }
    }

}
