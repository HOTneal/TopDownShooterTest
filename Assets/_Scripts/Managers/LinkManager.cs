﻿using Controllers;
using Controllers.BulletsController;
using Controllers.GrenadeController;
using Controllers.HelthbarController;
using Controllers.KillListController;
using Controllers.MobileController;
using Controllers.PoolController;
using Controllers.RespawnController;
using Controllers.ShootingController;
using Unit;
using UnityEngine;

namespace Managers
{
    public class LinkManager : MonoBehaviour
    {
        #region Singltone
        private static LinkManager _instance;
        public static LinkManager instance => _instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        public Unit.UnitController player;
        public MobileGrenadeController mobileGrenadeController;
        public MobileInputController mobilePlayerController;
        public KillListController killListController;
        public RespawnController respawnController;
        public GrenadeController grenadeController;
        public BulletController bulletController;
        public WeaponController weaponController;
        public DamageController damageController;
        public HelthController helthController;
        public DeadController deadController;
        public MobileShooting mobileShooting;
        public EventsManager eventsManager;
        public BulletsPool bulletsPool;
        public UnitsHolder unitsHolder;
        public PlayerPool playerPool;
        public UIManager uiManager;
        public BotsPool botsPool;
    }
}