using System;
using System.Collections;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class PlayerProjectile : Projectile
    {
        [Header("PowerUp")]
        [SerializeField] private PiercingShotPowerUp m_piercingShotPowerUp;
        [SerializeField] private IncreaseRangePowerUp m_increaseRangePowerUp;
        [SerializeField] private IncreaseBulletSizePowerUp m_increaseBulletSizePowerUp;

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
            transform.localScale = Vector3.one * m_increaseBulletSizePowerUp.CurrentScale;
            m_moveSpeed = m_initialSpeed - m_increaseBulletSizePowerUp.TotalSpeedReduce;
            base.SpawnProjectile(position, direction);
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
            m_piercingNumber = 0;
            base.OnDestroy();
        }
    }
}

