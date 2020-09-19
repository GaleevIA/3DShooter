using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class AmmoPanel : BaseObject
{
    private Gun gun;

    public void Refresh(GameObject currentWeapon)
    {
        if (currentWeapon.GetComponent<Gun>())
        {
            gun = currentWeapon.GetComponent<Gun>();
            GetComponent<Text>().text = $"{gun.curBulletCount} / {gun.maxBulletCount}";
        }
        else
            GetComponent<Text>().text = "";


    }
}
