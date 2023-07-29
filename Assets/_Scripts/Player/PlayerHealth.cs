using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class PlayerHealth : Health
    {
        [SerializeField] private PlayerComponentSet m_componentSet;
        
        protected override void Initialize()
        {
            m_curHealth = m_maxHealth;
            base.Initialize();
            m_componentSet.SetHealth(this);
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Test Damage")]
        private void TestDamage()
        {
            TakeDamage(15, null);
        }
        
        #endif
        
        public override void TakeDamage(int damage, GameObject instigator)
        {
            if (!AuthorizeTakingDamage()) return;

            m_curHealth -= damage;

            if (m_healthEvent != null)
            {
                m_healthEvent.Raise(m_curHealth/m_maxHealth);
            }

            if (m_curHealth >= 0)
            {
                StartCoroutine(OnInvulnerable());
                return;
            }

            ProcessKill();
            base.TakeDamage(damage, instigator);
        }

        protected override bool AuthorizeTakingDamage()
        {
            if (m_isInvulnerable)
            {
                return false;
            }
            return true;
        }
    } 
}

