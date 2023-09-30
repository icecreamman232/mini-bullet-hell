
using System;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class RailDamageHandler : DamageHandler
    {
        [SerializeField] private int m_piercingTargetAmount;
        [SerializeField] private float m_damageReducePerTarget;
        
        public Action<GameObject> OnFinalHit; 
        private int m_piercingCounter;

        private void OnEnable()
        {
            m_piercingCounter = 0;
        }

        protected override float GetDamage()
        {
            if (m_piercingCounter > 1)
            {
                var rawDamage = base.GetDamage();
                //Only reducing on second and over piercing target
                var percentReduce = MathHelpers.PercentOf(((m_piercingCounter - 1) * m_damageReducePerTarget));
                return rawDamage * (1 - percentReduce);
            }
            return base.GetDamage();
        }

        protected override void CauseDamage(Collider2D target)
        {
            var targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                m_piercingCounter++;
                targetHealth.TakeDamage(GetDamage(), this.gameObject, m_damageMultiplier > 1.0f);
                OnHit?.Invoke(target.gameObject);
                if (m_piercingCounter >= m_piercingTargetAmount)
                {
                    OnFinalHit?.Invoke(target.gameObject);
                    m_piercingCounter = 0;
                }
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

