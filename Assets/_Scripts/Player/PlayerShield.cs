using System;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerShield : MonoBehaviour
    {
        [SerializeField] private float m_knockBackForce;
        [SerializeField] private float m_knockBackDuration;
        [SerializeField] private LayerMask m_targetLayerMask;

        public Action OnShieldHit;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetLayerMask))
            {
                return;
            }

            if (other.gameObject.layer == LayerManager.EnemyLayer)
            {
                HandleEnemy(other);
                return;
            }

            if (other.gameObject.layer == LayerManager.EnemyProjectileLayer)
            {
                HandleEnemyBullet(other);
            }
        }

        private void HandleEnemy(Collider2D other)
        {
            var enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            var knockBackDir = (other.transform.position - transform.position).normalized; 
            enemyMovement.ApplyKnockBack(knockBackDir,m_knockBackForce, m_knockBackDuration);
            OnShieldHit?.Invoke();
        }

        private void HandleEnemyBullet(Collider2D other)
        {
            var projectile = other.gameObject.GetComponentInParent<Projectile>();
            projectile.DestroyBullet(null);
            OnShieldHit?.Invoke();
        }
    }
}

