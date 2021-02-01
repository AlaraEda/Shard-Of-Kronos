using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
public class BuildChecker : MonoBehaviour
{
    public string buildVersion;

    private string pathBuildVersion;
    private string pathSave;

    public BuildStruct buildStruct;
    
    public static readonly string saveFileNameBuildChecker = "buildversion.json";
    public static readonly string saveFileName = "playersave.json";

    public TMP_Text versionBuildText; 
    
    public void Awake()
    {
        pathBuildVersion = Path.Combine(Application.persistentDataPath, saveFileNameBuildChecker);
        
        pathSave = Path.Combine(Application.persistentDataPath, saveFileName);
        
        Debug.Log(pathBuildVersion);
        CheckBuild();

        versionBuildText.text = "Update " + Application.version.ToString();
    }

    public void CheckBuild()
    {
        //if there is a buildVersion.json al ready
        if (File.Exists(string.Concat(pathBuildVersion)))
        {
            Debug.Log("File exist");
            var json = File.ReadAllText(pathBuildVersion);
            var obj = JsonUtility.FromJson<BuildStruct>(json);

            //if this is a old BuildVersion
            if (buildVersion != obj.bVersion)
            {
                Debug.Log("This build is a old version");
                
                buildStruct = new BuildStruct(this);
                
                var jsonBuild = JsonUtility.ToJson(buildStruct, true);
                //Debug.LogWarning($"Save file JSON:\n{json}" );

                File.WriteAllText(pathBuildVersion, jsonBuild);
                
                //if this is a old BuildVersion & a save path exist
                if (File.Exists(string.Concat(pathSave)))
                {
                    Debug.Log("Delete old save file");
                    File.Delete(string.Concat(pathSave));
                }
                
                Debug.Log(jsonBuild);
           
            }
            else
            {
                Debug.Log("This is the same version");
            }
           
        }
        else
        {
            Debug.Log("This is a clean install");
            buildStruct = new BuildStruct(this);
            var json = JsonUtility.ToJson(buildStruct, true);
            File.WriteAllText(pathBuildVersion, json);
            Debug.Log(json);
            
            if (File.Exists(string.Concat(pathSave)))
            {
                Debug.Log("Delete old save file");
                File.Delete(string.Concat(pathSave));
            }
        }
    }
    
    public struct BuildStruct
    {
        public string bVersion;

        public BuildStruct(BuildChecker buildChecker)
        {
            bVersion = buildChecker.buildVersion;
        }
    }
    
    public struct ResetSave
    {
        
    }
}