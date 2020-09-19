using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponManager : BaseObject
{
    public  Single          flashlightReloadTime = 5;
    public  Flashlight      flashlight;
    private Light           light;
    public  FlashlightPanel flashLightPanel;
    public  AmmoPanel       ammoPanel;
    private GameObject      currentWeapon; 

    private Int32 weaponID = 0;

    void Start()
    {
        SelectWeapon();

        light = flashlight.GetComponentInChildren<Light>();
    }

    void Update()
    {
        UpdateWeapon();

        UpdateFlashlightCharge();

        flashLightPanel.Refresh();
        ammoPanel.Refresh(currentWeapon);
    }

    private void SelectWeapon()
    {
        Int32 i = 0;
        foreach (Transform weapon in gameObjectTransform)
        {
            weapon.gameObject.SetActive(i == weaponID);

            if (i == weaponID)
                currentWeapon = weapon.gameObject;

            i++;
        }
    }
 
    private void UpdateWeapon()
    {
        Int32 prevSelectedWeapon = weaponID;

        Single axisDirection = Input.GetAxis("Mouse ScrollWheel");

        if (axisDirection < 0)
        {
            if (weaponID <= 0)
                weaponID = ChildCount - 1;
            else
                weaponID--;
        }
        if (axisDirection > 0)
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
        if (Input.GetKeyDown(KeyCode.Alpha4) & ChildCount > 3)
            weaponID = 3;

        if (prevSelectedWeapon != weaponID)
            SelectWeapon();

        if (!flashlight.isActiveAndEnabled)
            light.enabled = false;
    }

    private void UpdateFlashlightCharge()
    {
        if (light.enabled & flashlightReloadTime > 0)
            flashlightReloadTime -= Time.deltaTime / 2;
        else if (!light.enabled & flashlightReloadTime < 5)
            flashlightReloadTime += Time.deltaTime;
    }
}
