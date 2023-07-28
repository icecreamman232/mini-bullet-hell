using System;
using JustGame.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Weapons
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] private int m_minDamageCause;
        [SerializeField] private int m_maxDamageCause;
        [SerializeField] private LayerMask m_targetMask;

        public Action OnHit;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            
            CauseDamage(other);
        }

        protected virtual int GetDamage()
        {
            return Random.Range(m_minDamageCause, m_maxDamageCause + 1);
        }
        
        protected virtual void CauseDamage(Collider2D target)
        {
            var targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                OnHit?.Invoke();
                targetHealth.TakeDamage(GetDamage(), this.gameObject);
            }
        }
    } 
}

