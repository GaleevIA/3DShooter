using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseWeapon : BaseObject
{
    [SerializeField] protected Transform        gunT;
    [SerializeField] protected Single           force = 300;
    [SerializeField] protected ParticleSystem   muzzleFlash;
    [SerializeField] protected GameObject       hitParticle;

    protected Timer     rechargeTimer   = new Timer();
    protected Boolean   fire            = true;

    public abstract void Fire();

    protected override void Awake()
    {
        base.Awake();
        gunT = gameObjectTransform.GetChild(2);
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void Update()
    {
        rechargeTimer.Update();

        if (rechargeTimer.IsEvent())
            fire = true;
    }
}
