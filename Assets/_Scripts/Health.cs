using System;
using System.Collections;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float m_maxHealth;
        [SerializeField] protected float m_curHealth;
        
        [Header("Flicking color")]
        [SerializeField] protected float m_invulnerableDuration;

        [SerializeField] protected Color m_flickingColor;
        [SerializeField] protected float m_delayBetweenFlicks;

        protected SpriteRenderer m_spriteRenderer;
        protected Color m_initColor;
        protected bool m_isInvulnerable;
        public Action OnDeath;
        
        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_curHealth = m_maxHealth;
            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            m_initColor = m_spriteRenderer.color;
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

            var flickStop = Time.time + m_invulnerableDuration;
            while (Time.time < flickStop)
            {
                m_spriteRenderer.material.color = m_flickingColor;
                yield return new WaitForSeconds(m_delayBetweenFlicks);
                m_spriteRenderer.material.color = m_initColor;
                yield return new WaitForSeconds(m_delayBetweenFlicks);
            }
            m_spriteRenderer.material.color = m_initColor;
            
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
