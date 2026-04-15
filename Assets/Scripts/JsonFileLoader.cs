using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

public static class JsonFileLoader
{
    private static readonly string JsonDirectory = Path.Combine(Application.persistentDataPath, "json");

    public static bool LoadFile<T>(string fileName, out T obj)
    {
        if (!Directory.Exists(JsonDirectory))
            Directory.CreateDirectory(JsonDirectory);
        
        if (!File.Exists(GetPathToFile(fileName)))
        {
            obj = default;
            return false;
        }

        string json = File.ReadAllText(GetPathToFile(fileName));
        obj = JsonConvert.DeserializeObject<T>(json);
        return true;
    }

    public static void SaveFile(string fileName, [CanBeNull] object obj)
    {
        if (!Directory.Exists(JsonDirectory))
            Directory.CreateDirectory(JsonDirectory);
        
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        using StreamWriter writer = File.CreateText(GetPathToFile(fileName));
        writer.Write(json);
    }

    private static string GetPathToFile(string fileName) => Path.Combine(JsonDirectory, fileName);
}