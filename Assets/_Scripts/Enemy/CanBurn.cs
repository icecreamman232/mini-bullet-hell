using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class CanBurn : MonoBehaviour
    {
        [SerializeField] private BurningCoatingPowerUp m_burningCoatingPowerUp;
        [SerializeField] private EnemyHealth m_health;
        [SerializeField] private ParticleSystem m_burningVFX;

        private float m_damageTimer;
        private float m_durationTimer;

        private void Start()
        {
            m_burningVFX.Stop();
        }
        
        public void ApplyBurn()
        {
            //if enemy got burning and got apply burn again! We reset the duration and continue burning them!
            m_durationTimer = m_burningCoatingPowerUp.Duration;
            m_damageTimer = m_burningCoatingPowerUp.DamageFrequency;
            if (m_burningVFX.isStopped)
            {
                m_burningVFX.Play();
            }
        }

        private void RemoveBurn()
        {
            m_burningVFX.Stop();
        }
        
        private void Update()
        {
            if (!m_burningCoatingPowerUp.IsActive) return;

            m_damageTimer -= Time.deltaTime;
            m_durationTimer -= Time.deltaTime;
            if (m_durationTimer <= 0)
            {
                m_durationTimer = 0;
                RemoveBurn();
                return;
            }

            if (m_damageTimer <= 0)
            {
                DealBurnDamage();
                m_damageTimer = m_burningCoatingPowerUp.DamageFrequency;;
            }
        }

        private void DealBurnDamage()
        {
            m_health.TakeDamage(m_burningCoatingPowerUp.BurnDamage,this.gameObject);
        }
    }
}

