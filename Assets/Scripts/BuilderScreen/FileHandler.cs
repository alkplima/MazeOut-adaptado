using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class FileHandler
{

    public static void SaveToJSON<T>(List<T> toSave, string filename)
    {
        Debug.Log(GetPath(filename));
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
#if (UNITY_WEBGL && (!UNITY_EDITOR))
        EscritaWebGL(GetPath(filename), content);
#else
        WriteFile(GetPath(filename), content);
#endif
    }

    public static void SaveToJSON<T>(T toSave, string filename)
    {
        string content = JsonUtility.ToJson(toSave);
#if (UNITY_WEBGL && (!UNITY_EDITOR))
        EscritaWebGL(GetPath(filename), content);
#else
        WriteFile(GetPath(filename), content);
#endif
    }

    public static List<T> ReadListFromJSON<T>(string filename)
    {
#if (UNITY_WEBGL && (!UNITY_EDITOR))
        string content = LeituraWebGL(GetPath(filename));
#else
        string content = ReadFile(GetPath(filename));
#endif

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        return res;

    }

    public static T ReadFromJSON<T>(string filename)
    {
#if (UNITY_WEBGL && (!UNITY_EDITOR))
        string content = LeituraWebGL(GetPath(filename));
#else
        string content = ReadFile(GetPath(filename));
#endif

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(content);

        return res;

    }

    private static string GetPath(string filename)
    {
#if (UNITY_WEBGL && (!UNITY_EDITOR))
        return Path.Combine(Application.persistentDataPath, filename);
#else
        return Path.Combine(Application.streamingAssetsPath, filename);
#endif
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
            writer.Write(content);
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    private static string LeituraWebGL(string path)
    {
        if (File.Exists(path))
            return File.ReadAllText(path);
        else return "";
    }

    private static void EscritaWebGL(string path, string content)
    {
        File.WriteAllText(path, content);
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
    }
}


public static class JsonHelper
{
    public static T[] FromJson<T> (string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
        return wrapper.Items;
    }

    public static string ToJson<T> (T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T> ();
        wrapper.Items = array;
        return JsonUtility.ToJson (wrapper);
    }

    public static string ToJson<T> (T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T> ();
        wrapper.Items = array;
        return JsonUtility.ToJson (wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}