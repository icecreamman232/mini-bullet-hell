using System;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Weapons
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] private ShipAttributeRuntime m_attributeRuntime;
        [SerializeField] protected float m_knockBackForce;
        [SerializeField] protected float m_knockBackDuration;
        [SerializeField] protected float m_damageMultiplier;
        [SerializeField] protected int m_minDamageCause;
        [SerializeField] protected int m_maxDamageCause;
        [SerializeField] protected LayerMask m_targetMask;

        public int MinDamage => m_minDamageCause;
        public int MaxDamage => m_maxDamageCause;
        
        /// <summary>
        /// Callback when hitting target
        /// </summary>
        public Action<GameObject> OnHit;

        protected virtual void Start()
        {
            if (m_attributeRuntime != null)
            {
                m_minDamageCause = m_attributeRuntime.MinAtkDamage;
                m_maxDamageCause = m_attributeRuntime.MaxAtkDamage;
            }
            
            SetDamageMultiplier(1);
        }
        
        public virtual void SetDamage(int minDamage, int maxDamage)
        {
            m_minDamageCause = minDamage;
            m_maxDamageCause = maxDamage;
        }
        
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

        public virtual void AddDamageMultiplier(float addValue)
        {
            m_damageMultiplier += addValue;
        }
        
        protected virtual void CauseDamage(Collider2D target)
        {
            var targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                OnHit?.Invoke(target.gameObject);
                targetHealth.TakeDamage(GetDamage(), this.gameObject, m_damageMultiplier > 1.0f);

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

