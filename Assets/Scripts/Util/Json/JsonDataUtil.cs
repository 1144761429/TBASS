using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public enum EPersistentDataPath
{
    SystemAppData,
    UnityAsset
}

public class JsonDataUtil : IDataService
{
    public T LoadData<T>(EPersistentDataPath mainPath, string relativePath)
    {
        string path = "";
        switch (mainPath)
        {
            case EPersistentDataPath.UnityAsset:
                path = @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\" + relativePath;
                break;
            case EPersistentDataPath.SystemAppData:
                path = Application.persistentDataPath + relativePath;
                break;
        }

        if (!File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}. File does not exist.");
            throw new FileNotFoundException($"{path} does not exist.");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }

    public bool SaveData<T>(EPersistentDataPath mainPath, string relativePath, T data)
    {
        string path = "";
        switch (mainPath)
        {
            case EPersistentDataPath.UnityAsset:
                path = @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\" + relativePath;
                break;
            case EPersistentDataPath.SystemAppData:
                path = Application.persistentDataPath + relativePath;
                break;
        }

        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one.");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file for the first time");
            }
            FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }

    }
}
