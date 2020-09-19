using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class JSONData : ISaveData
{
    string _path = Path.Combine(Application.dataPath, "JSONData.xml");

    public PlayerData Load()
    {
        string temp = File.ReadAllText(_path);
        return JsonUtility.FromJson<PlayerData>(temp);
    }

    public void Save(PlayerData player)
    {
        string fileJson = JsonUtility.ToJson(player);
        File.WriteAllText(_path, fileJson);
    }
}
