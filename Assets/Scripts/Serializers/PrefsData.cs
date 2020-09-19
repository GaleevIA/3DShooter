using System;
using UnityEngine;

public class PrefsData : ISaveData
{
    public PlayerData Load()
    {
        PlayerData result = new PlayerData();

        result.name = PlayerPrefs.GetString("Name");
        result.health = PlayerPrefs.GetInt("Health");
        result.isVisible = PlayerPrefs.GetInt("IsVisible") == 1 ? true : false;

        return result;
    }

    public void Save(PlayerData player)
    {
        PlayerPrefs.SetString("Name", player.name);
        PlayerPrefs.SetInt("Health", player.health);
        PlayerPrefs.SetInt("IsVisible", player.isVisible ? 1 : 0);
        PlayerPrefs.Save();
    }
}
