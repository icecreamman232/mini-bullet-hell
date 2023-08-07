using System;
using System.Collections;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float m_maxHealth;
        [SerializeField] protected float m_curHealth;
        [SerializeField] protected float m_invulnerableDuration;
        protected bool m_isInvulnerable;
        public Action OnDeath;
        
        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_curHealth = m_maxHealth;
        }

        public virtual void TakeDamage(int damage, GameObject instigator)
        {
            if (!AuthorizeTakingDamage()) return;

            m_curHealth -= damage;


            UpdateUI();

            if (m_curHealth >= 0)
            {
                StartCoroutine(OnInvulnerable());
                return;
            }

            ProcessKill();
        }

        protected virtual bool AuthorizeTakingDamage()
        {
            return true;
        }
        
        protected virtual IEnumerator OnInvulnerable()
        {
            if (m_isInvulnerable)
            {
                yield break;
            }
            m_isInvulnerable = true;
            yield return new WaitForSeconds(m_invulnerableDuration);
            m_isInvulnerable = false;
        }

        protected virtual void UpdateUI()
        {
            
        }
        
        protected virtual void ProcessKill()
        {
            //place holder script
            OnDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

