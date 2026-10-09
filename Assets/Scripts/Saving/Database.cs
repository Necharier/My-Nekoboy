using UnityEngine;
using System.IO;

public class Database
{
    private string path;

    public Database()
    {
        
        path = Application.persistentDataPath + "/Lucky/Saves/";

        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("Создана папка для сохранений: " + path);
        }
    }

    public void SaveData<T>(string saveName, T saveData)
    {
        string jsonToSave = JsonUtility.ToJson(saveData);
        File.WriteAllText(path + saveName + ".json", jsonToSave);
    }

    public void LoadData<T>(string saveName, System.Action<T> callback)
    {
        string fullPath = path + saveName + ".json";

        if (File.Exists(fullPath))
        {
            string loadedJson = File.ReadAllText(fullPath);
            callback(JsonUtility.FromJson<T>(loadedJson));
        }
        else
        {
            Debug.Log("Файл не существует: " + fullPath);
        }
    }
}