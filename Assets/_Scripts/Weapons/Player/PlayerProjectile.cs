using System;
using System.Collections;
using JustGame.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Weapons
{
    public class PlayerProjectile : Projectile
    {
        [Header("PowerUp")]
        [SerializeField] private PiercingShotPowerUp m_piercingShotPowerUp;
        [SerializeField] private IncreaseRangePowerUp m_increaseRangePowerUp;
        [SerializeField] private IncreaseBulletSizePowerUp m_increaseBulletSizePowerUp;
        [SerializeField] private EnableCriticalDamagePowerUp m_criticalDamagePowerUp;
        
        private int m_piercingNumber;

        protected override void Awake()
        {
            base.Awake();
            if (m_increaseBulletSizePowerUp.IsActive)
            {
                transform.localScale = Vector3.one * m_increaseBulletSizePowerUp.CurrentScale;
                m_moveSpeed -= m_increaseBulletSizePowerUp.TotalSpeedReduce;
            }
        }

        protected override void Start()
        {
            base.Start();
            m_piercingShotPowerUp.OnApplyPowerUp += TriggerPiercingShotPowerUp;
            m_increaseRangePowerUp.OnApplyPowerUp += TriggerIncreaseRangePowerUp;
        }

        public override void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            if (m_criticalDamagePowerUp.IsActive)
            {
                CheckCriticalDamage();
            }
            
            transform.localScale = Vector3.one * m_increaseBulletSizePowerUp.CurrentScale;
            m_moveSpeed = m_initialSpeed - m_increaseBulletSizePowerUp.TotalSpeedReduce;
            base.SpawnProjectile(position, direction);
        }

        private void CheckCriticalDamage()
        {
            var critChance = Random.Range(0f, 100f);
            if (critChance > m_criticalDamagePowerUp.CritChance) return;
            
            var greatCritChance = Random.Range(0f, 100f);
            if (greatCritChance < m_criticalDamagePowerUp.GreatCritChance)
            {
                //Setup average crit damage
                m_spriteRenderer.color = m_criticalDamagePowerUp.AverageColor;
                m_damageHandler.SetDamageMultiplier(m_criticalDamagePowerUp.AverageCritMultiplier);
            }
            else
            {
                //Setup great crit damage
                m_spriteRenderer.color = m_criticalDamagePowerUp.GreatColor;
                m_damageHandler.SetDamageMultiplier(m_criticalDamagePowerUp.GreatCritMultiplier);
            }
        }
        
        private void TriggerIncreaseRangePowerUp()
        {
            m_maxDistanceTravel += m_increaseRangePowerUp.RangeIncreasePerTime;
        }
        
        private void TriggerPiercingShotPowerUp()
        {
            m_piercingShotPowerUp.IsActive = true;
        }

        protected override IEnumerator DestroyRoutine()
        {
            if (m_isDestroying)
            {
                yield break;
            }

            m_isDestroying = true;
            m_piercingNumber++;
            if (m_piercingNumber <= 1 && m_piercingShotPowerUp.IsActive)
            {
                m_isDestroying = false;
                yield break;
            }
            
            yield return new WaitForSeconds(m_delayBeforeDestruction);
            
            if (m_destroyAnim != null)
            {
                m_destroyAnim.SetTrigger();
                yield return new WaitForSeconds(m_destroyAnim.Duration);
            }
            OnDestroy();
        }

        protected override void OnDestroy()
        {
            
            m_damageHandler.SetDamageMultiplier(1);
            m_piercingNumber = 0;
            base.OnDestroy();
        }

        private void OnDisable()
        {
            m_spriteRenderer.color = Color.white;
        }
    }
}

