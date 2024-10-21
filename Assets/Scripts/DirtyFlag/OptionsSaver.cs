using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class OptionsSaver : MonoBehaviour
{
    private string filePath;
    private Dictionary<string, object> saveableOptions;

    void Awake()
    {
        filePath = $"{Application.dataPath}/Options";
        Restore();
    }

    public void Save()
    {
        saveableOptions = new Dictionary<string, object>();
        GameObject[] sceneGO = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in sceneGO)
        {
            ISaveableOption saveableOption = go.GetComponent<ISaveableOption>();
            if (saveableOption != null)
            {
                saveableOptions[go.name] = saveableOption.GetData();
            }
        }
        BinaryFormatter formatter = new();
        FileStream fileStream = File.Create(filePath);
        formatter.Serialize(fileStream, saveableOptions);
        fileStream.Flush();
        fileStream.Close();
        Debug.Log("Opciones guardadas");
    }

    private void Restore()
    {
        Debug.Log("Restaurando archivos guardados...");
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new();
            FileStream fileStream = File.OpenRead(filePath);
            saveableOptions = (Dictionary<string, object>)formatter.Deserialize(fileStream);
            fileStream.Close();
            foreach (var obj in saveableOptions)
            {
                GameObject sceneObj = GameObject.Find(obj.Key);
                if (sceneObj != null)
                {
                    ISaveableOption saveableObj = sceneObj.GetComponent<ISaveableOption>();
                    if (saveableObj != null)
                    {
                        saveableObj.RestoreData(obj.Value);
                    }
                }
            }
            Debug.Log("Opciones restauradas");
        }
        else
        {
            Debug.Log("Fichero no encontrado");
        }
    }
}
