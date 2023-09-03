using System;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Weapons
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] protected float m_knockBackForce;
        [SerializeField] protected float m_knockBackDuration;
        [SerializeField] protected float m_damageMultiplier;
        [SerializeField] protected int m_minDamageCause;
        [SerializeField] protected int m_maxDamageCause;
        [SerializeField] protected LayerMask m_targetMask;
        
        public Action<GameObject> OnHit;
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            
            CauseDamage(other);
        }

        protected virtual float GetDamage()
        {
            return Random.Range(m_minDamageCause, m_maxDamageCause + 1) * m_damageMultiplier;
        }

        public virtual void SetDamageMultiplier(float newValue)
        {
            m_damageMultiplier = newValue;
        }
        
        protected virtual void CauseDamage(Collider2D target)
        {
            var targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                OnHit?.Invoke(target.gameObject);
                targetHealth.TakeDamage(GetDamage(), this.gameObject);

                if (target.gameObject.layer == LayerManager.EnemyLayer)
                {
                    var enemyMovement = target.gameObject.GetComponent<EnemyMovement>();
                    var knockBackDir = (target.transform.position - transform.position).normalized; 
                    enemyMovement.ApplyKnockBack(knockBackDir,m_knockBackForce, m_knockBackDuration);
                }
            }
            
            //Destroy bullet on hit
            if (target.gameObject.layer == LayerManager.PlayerProjectileLayer
                && this.gameObject.layer == LayerManager.EnemyProjectileLayer)
            {
                var projectile = target.gameObject.GetComponentInParent<Projectile>();
                projectile.DestroyBullet(null);
            }
        }
    } 
}

