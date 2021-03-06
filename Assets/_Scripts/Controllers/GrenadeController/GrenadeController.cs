﻿using System.Collections;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeController : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 20.0f;
        [SerializeField] private float m_ExplodeTime = 1.0f;
        [SerializeField] private DataWeapons m_Grenade;
        [SerializeField] private GrenadeEffects m_Effects;

        private LinkManager m_LinkManager;
    
        private void Start()
        {
            m_LinkManager = LinkManager.instance;
        }
    
        public IEnumerator ExplodeGrenade(Transform grenade)
        {
            yield return new WaitForSeconds(m_ExplodeTime);
            
            Collider[] allUnitsInRadius = Physics.OverlapSphere(grenade.position, m_Radius);
        
            foreach (var nearbyObj in allUnitsInRadius)
            {
                if (nearbyObj.TryGetComponent(out Unit.UnitController unit))
                    RayCastForDamage(unit, grenade);
            }
        
            m_Effects.GenerateEffects(grenade);
            Destroy(grenade.gameObject);
        }

        private void RayCastForDamage(Unit.UnitController nearbyUnit, Transform grenade)
        {
            RaycastHit hit;
            Transform nearbyUnitTarget = nearbyUnit.pointForDamage;
            Vector3 pos = nearbyUnitTarget.position - grenade.position;
            float distance = pos.magnitude;
            Vector3 targetDirection = pos / distance;

            if (!Physics.Raycast(grenade.position, targetDirection, out hit)) return;
            
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Bot"))
                Damage(m_LinkManager.player, nearbyUnit, m_Grenade);
        }

        private void Damage(Unit.UnitController unit, Unit.UnitController damagedUnit, DataWeapons weapon)
        {
            m_LinkManager.damageController.Damage(damagedUnit, weapon);
            m_LinkManager.helthController.CheckLiveUnit(unit, damagedUnit, weapon);
        }
    }
}