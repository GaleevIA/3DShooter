using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ChangeWeapon : BaseObject
{
    private Int32 weaponID = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        Int32 prevSelectedWeapon = weaponID;

        Single axisDirection = Input.GetAxis("Mouse ScrollWheel");

        if(axisDirection < 0)
        {
            if (weaponID <= 0)
                weaponID = ChildCount - 1;
            else
                weaponID--;
        }
        if(axisDirection > 0)
        {
            if (weaponID >= ChildCount - 1)
                weaponID = 0;
            else
                weaponID++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponID = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) & ChildCount > 1)
            weaponID = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) & ChildCount > 2)
            weaponID = 2;
        if(Input.GetKeyDown(KeyCode.Alpha4) & ChildCount > 3)
            weaponID = 3;

        if (prevSelectedWeapon != weaponID)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        Int32 i = 0;
        foreach(Transform weapon in gameObjectTransform)
        {
            weapon.gameObject.SetActive(i == weaponID);

            i++;
        }
    }
}
