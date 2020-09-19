using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISaveData
{
    void Save(PlayerData player);

    PlayerData Load();
}
