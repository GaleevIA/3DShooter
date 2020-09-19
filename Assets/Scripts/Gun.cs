using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : BaseWeapon
{
    public Int32 maxBulletCount;
    public Single shootDistance;
    public Int32 damage;
    public Int32 curBulletCount;
    private KeyCode reloadKey = KeyCode.R;

    protected override void Awake()
    {
        base.Awake();
        curBulletCount = maxBulletCount;
    }

    protected override void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            Fire();
        if (Input.GetKeyDown(reloadKey))
            curBulletCount = maxBulletCount;
    }

    private void SetDamage(ISetDamage obj)
    {
        obj?.SetDamage(damage);
    }

    public override void Fire()
    {
        if(curBulletCount > 0 & fire)
        {
            muzzleFlash.Play();
            curBulletCount--;
            RaycastHit hit;
            if(Physics.Raycast(GetRay(), out hit, shootDistance))
            {
                if (hit.collider.tag == "Player")
                    return;
                else
                    SetDamage(hit.collider.GetComponentInParent<ISetDamage>());

                PlayHitParticle(hit);
            }
        }
    }

    private Ray GetRay()
    {
        return new Ray(MainCamera.transform.position, MainCamera.transform.forward);
    }

    private void PlayHitParticle(RaycastHit hit)
    {
        GameObject tempHit = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
        tempHit.transform.parent = hit.transform;
        Destroy(tempHit, 0.5f);
    }
}
