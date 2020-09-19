using System;
using System.IO;
using UnityEngine;

public class StreamData : ISaveData
{
    string _path = Path.Combine(Application.dataPath, "Stream.xyz");

    public PlayerData Load()
    {
        var result = new PlayerData();

        if (!File.Exists(_path))
            return result;

        using (StreamReader tmpStreamRd = new StreamReader(_path))
        {
            while (!tmpStreamRd.EndOfStream)
            {
                result.name = tmpStreamRd.ReadLine();
                Int32.TryParse(tmpStreamRd.ReadLine(), out result.health);
                Boolean.TryParse(tmpStreamRd.ReadLine(), out result.isVisible);
            }
        }

        return result;
    }

    public void Save(PlayerData player)
    {
        using (StreamWriter tmpStreamWs = new StreamWriter(_path))
        {
            tmpStreamWs.WriteLine(player.name);
            tmpStreamWs.WriteLine(player.health);
            tmpStreamWs.WriteLine(player.isVisible);
        }
    }
}
