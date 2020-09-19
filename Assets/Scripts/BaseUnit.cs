using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseUnit : BaseObject, ISetDamage
{
    [SerializeField][Header("Количество единиц здоровья")] protected Int32 health;

    public Int32 Health
    {
        get { return health; }
        set { health = value; }
    }

    void ISetDamage.SetDamage(int damage)
    {
        Health -= damage;
    }
}
