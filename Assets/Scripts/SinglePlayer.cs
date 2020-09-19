using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerData
{
    public string name;
    public int health;
    public bool isVisible;
    public override string ToString() => $"Name{name} Health{health} Visible{isVisible}";
}

public class SinglePlayer : BaseUnit
{
    private ISaveData _data;

    void Start()
    {
        health = 100;
        _data = new PrefsData();

        PlayerData singlePlayer = new PlayerData
        {
            health = health,
            name = name,
            isVisible = isVisible
        };

        _data.Save(singlePlayer);
        PlayerData newPlayer = _data.Load();
    }


    void Update()
    {
        
    }
}
