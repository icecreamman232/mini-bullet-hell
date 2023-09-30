using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class PlayerExplosiveProjectile : Projectile
    {
        protected override void Start()
        {
            base.Start();
            ((ExplosiveDamageHandler)m_damageHandler).OnSelfExplode += DestroyBullet;
        }

        public void StopBullet()
        {
            m_moveDirection = Vector2.zero;
        }
        
        protected override void CheckTravelDistance()
        {
            if (m_moveDirection == Vector2.zero) return;
            
            m_distanceTraveled = Vector2.Distance(m_originalPos, transform.position);
            if (m_distanceTraveled >= m_maxDistanceTravel)
            {
                m_moveDirection = Vector2.zero;
                ((ExplosiveDamageHandler)m_damageHandler).StartTimer();
            }
        }

        protected override void OnDestroy()
        {
            ((ExplosiveDamageHandler)m_damageHandler).OnSelfExplode -= DestroyBullet;
            base.OnDestroy();
        }
    }
}

