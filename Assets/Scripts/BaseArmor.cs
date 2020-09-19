using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseArmor : BaseObject, ISetDamage
{
    [Header("Количество единиц брони")] public Int32 armor;

    public void SetDamage(Int32 damage)
    {
        BaseUnit unit = GetComponentInParent<BaseUnit>();

        unit.Health = Math.Max(0, unit.Health + Math.Min(armor - damage, 0));

        if (unit.Health == 0 & tag != "Player")
            Destroy(unit.gameObject);
    }
}
