﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private AudioClip m_SoundReload;
    [SerializeField] private AudioSource m_AudioSource;

    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    public void BulletsCount(Unit unit)
    {
        BulletsQuantity bulletsQuantity = unit.m_BulletsQuantity;
        if (bulletsQuantity.m_QuantityBulletsInClip == 0) return;
        
        bulletsQuantity.m_QuantityBulletsInClip--;
        
        if (bulletsQuantity.m_QuantityBulletsInClip == 0 & bulletsQuantity.m_AllBulletsWeapon != 0)
            StartCoroutine(ReloadWeapon(unit));
            
        if (bulletsQuantity.m_QuantityBulletsInClip == 0 & bulletsQuantity.m_AllBulletsWeapon == 0)
            NoBullets(unit);
    }

    private IEnumerator ReloadWeapon(Unit unit)
    {
        BulletsQuantity bulletsQuantity = unit.m_BulletsQuantity;
        ShootingCheck shootingCheck = unit.m_ShootingCheck;
        Animator animator = unit.m_Animator;
        
        m_LinkManager.m_ShootingController.StopAllCoroutines();
        shootingCheck.StopAllCoroutines();
        shootingCheck.DisableShooting();
        animator.SetTrigger("ReloadGun");
        m_AudioSource.PlayOneShot(m_SoundReload);
        
        yield return new WaitForSeconds(bulletsQuantity.m_CurrentWeapon.ReloadTime);
        
        if (!bulletsQuantity.m_CurrentWeapon.InfinityBullets)
            bulletsQuantity.m_AllBulletsWeapon -= bulletsQuantity.m_DefaultBulletsInClip;
        
        m_AudioSource.Stop();
        bulletsQuantity.m_QuantityBulletsInClip = bulletsQuantity.m_DefaultBulletsInClip;
        LinkManager.Instance.m_UIManager.SetQuantityBullets();
        shootingCheck.EnableShooting();
    }

    private void NoBullets(Unit unit)
    {
        unit.m_ShootingCheck.isNoAmmo = true;
        unit.m_ShootingCheck.m_ModeCanShooting = ShootingCheck.ModeCanShooting.NoAmmo;
    }
}