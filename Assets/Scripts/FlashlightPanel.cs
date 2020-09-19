using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlashlightPanel : MonoBehaviour
{
    public Transform[] charges;
    public GameObject flashLight;

    public GameObject weapons;
    private WeaponManager weaponManager;

    void Awake()
    {
        weaponManager = weapons.GetComponent<WeaponManager>();
    }

    void Start()
    {
        charges = new Transform[transform.childCount];

        for (Int32 i = 0; i < charges.Length; i++)
            charges[i] = transform.GetChild(i);
    }

    public void Refresh()
    {
        for (Int32 i = 0; i < charges.Length; i++)
            charges[i].gameObject.SetActive(false);

        for (Int32 i = 0; i < (Int32)weaponManager.flashlightReloadTime; i++)
            charges[i].gameObject.SetActive(true);
    }
}
