using System.IO;
using System;
using UnityEngine;

public class SaveSystem 
{
    private const string FileName = "save.json";

    private static string SavePath => Path.Combine(Application.persistentDataPath, FileName);
    public static bool HasSave => File.Exists(SavePath);

    public static void Save(SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }
        catch (Exception exception)
        {
            Debug.LogError($"No se pudo guardar la partida: {exception.Message}");
        }
    }

    public static bool TryLoad(out SaveData data)
    {
        data = null;

        if (!File.Exists(SavePath))
            return false;

        try
        {
            string json = File.ReadAllText(SavePath);
            data = JsonUtility.FromJson<SaveData>(json);
            return data != null;
        }
        catch (Exception exception)
        {
            Debug.LogError($"No se pudo cargar la partida: {exception.Message}");
            return false;
        }
    }

    public static void Delete()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
