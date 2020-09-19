using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Flashlight : BaseObject
{
    [SerializeField][Header("Свет фонаря")] private Light light;

    private KeyCode control = KeyCode.F;
    private Single timeout = 10;
    private Single currentTime;
    private Single currentReloadTime;

    public GameObject weapons;
    private WeaponManager weaponManager;
    
    private Material lightMat;
    void Start()
    {
        light = GetComponentInChildren<Light>();
        lightMat = GetMaterial;
        weaponManager = weapons.GetComponent<WeaponManager>();
    }
    private void ActivateFlashlight(Boolean val)
    {
        if (currentReloadTime >= 5)
            light.enabled = val;
        else
            light.enabled = false;
    }
    private void CheckCharge()
    {
        currentReloadTime = weaponManager.flashlightReloadTime;

        if (light.enabled)
        {
            currentTime += Time.deltaTime;           
            if (currentTime > timeout)
            {
                currentTime = 0;
                ActivateFlashlight(false);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(control))
            ActivateFlashlight(!light.enabled);

        CheckCharge();       
    }
}
