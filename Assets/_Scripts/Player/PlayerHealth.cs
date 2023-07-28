using System.Collections;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerHealth : PlayerAbility
    {
        [SerializeField] private FloatEvent m_healthEvent;
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private float m_invulnerableDuration;
        private bool m_isInvulnerable;

        public override void Initialize()
        {
            base.Initialize();
            m_curHealth = m_maxHealth;
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Test Damage")]
        private void TestDamage()
        {
            TakeDamage(15, null);
        }
        
        #endif
        
        public void TakeDamage(int damage, GameObject instigator)
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
        }

        private bool AuthorizeTakingDamage()
        {
            if (m_isInvulnerable)
            {
                return false;
            }
            return true;
        }

        private IEnumerator OnInvulnerable()
        {
            if (m_isInvulnerable)
            {
                yield break;
            }
            m_isInvulnerable = true;
            yield return new WaitForSeconds(m_invulnerableDuration);
            m_isInvulnerable = false;
        }
        
        private void ProcessKill()
        {
            //place holder script
            gameObject.SetActive(false);
        }
    } 
}

