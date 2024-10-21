using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] float savingInterval;
    private bool canSave = true; //Booleano que se pone en false en casos en los que no se debería guardar (zonas no seguras)
    private bool dirty = false;
    float timer = 0;
    private Dictionary<string, object> saveableObjects;
    
    void Awake()
    {
        Restore($"{Application.dataPath}/{fileName}");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= savingInterval)
        {
            timer = 0;
            if(canSave) Save($"{Application.dataPath}/{fileName}");
        }
    }

    private void SetDirty()
    {
        dirty = true;
    }

    public void SetCanSave(bool canSave)
    {
        this.canSave = canSave;
    }

    private void Save(string filePath)
    {
        saveableObjects = new Dictionary<string, object>();
        GameObject[] sceneGO = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in sceneGO)
        {
            ISaveableObject saveableObject = go.GetComponent<ISaveableObject>();
            if (saveableObject != null)
            {
                saveableObjects[go.name] = saveableObject.GetData();
                if (saveableObject.IsDirty())
                {
                    SetDirty();
                    saveableObject.SetDirty(false);
                }
            }
        }

        if (dirty)
        {
            BinaryFormatter formatter = new();
            FileStream fileStream = File.Create(filePath);
            formatter.Serialize(fileStream, saveableObjects);
            fileStream.Flush();
            fileStream.Close();
            dirty = false;
            Debug.Log("Juego guardado");
        }
    }

    private void Restore(string filePath)
    {
        Debug.Log("Restaurando archivos guardados...");
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new();
            FileStream fileStream = File.OpenRead(filePath);
            saveableObjects = (Dictionary<string, object>)formatter.Deserialize(fileStream);
            fileStream.Close();
            foreach (var obj in saveableObjects)
            {
                GameObject sceneObj = GameObject.Find(obj.Key);
                if (sceneObj != null)
                {
                    ISaveableObject saveableObj = sceneObj.GetComponent<ISaveableObject>();
                    if (saveableObj != null)
                    {
                        saveableObj.RestoreData(obj.Value);
                    }
                }
            }
            Debug.Log("Archivos restaurados");
        }
        else
        {
            Debug.Log("Fichero no encontrado");
        }
    }

    public void ForceSave()
    {
        Save($"{Application.dataPath}/{fileName}");
    }
}
