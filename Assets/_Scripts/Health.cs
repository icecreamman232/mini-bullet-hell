using System;
using System.Collections;
using JustGame.Scripts.Attribute;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class Health : MonoBehaviour
    {
        [SerializeField][ReadOnly] protected float m_maxHealth;
        [SerializeField] protected float m_curHealth;
        [SerializeField] protected float m_delayBeforeDeath;
        [Header("Flicking color")]
        [SerializeField] protected float m_invulnerableDuration;

        [SerializeField] protected Color m_flickingColor;
        [SerializeField] protected float m_delayBetweenFlicks;

        protected SpriteRenderer m_spriteRenderer;
        protected Collider2D m_collider;
        protected Color m_initColor;
        protected bool m_isDead;
        protected bool m_isInvulnerable;

        public bool IsDead => m_isDead;

        public Action OnHit;
        public Action OnDeath;
        
        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_curHealth = m_maxHealth;
            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            m_collider = GetComponent<Collider2D>();
            m_initColor = m_spriteRenderer.color;
        }

        protected virtual void Update()
        {
            
        }

        /// <summary>
        /// Compute final damage received after armor
        /// </summary>
        /// <returns></returns>
        protected virtual float ComputeFinalDamage(float rawDamage)
        {
            return rawDamage;
        }
        
        public virtual void TakeDamage(float damage, GameObject instigator, bool isCriticalHit = false, bool isInstantDead = false)
        {
            if (!AuthorizeTakingDamage()) return;

            
            
            m_curHealth -= ComputeFinalDamage(damage);
            
            OnHit?.Invoke();

            UpdateUI();

            if (m_curHealth >= 0)
            {
                StartCoroutine(OnInvulnerable());
                return;
            }

            ProcessKill(isInstantDead);
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
        
        protected virtual void ProcessKill(bool isInstantDead = false)
        {
            //place holder script
            OnDeath?.Invoke();
            StartCoroutine(KillRoutine(isInstantDead));
        }

        protected virtual IEnumerator KillRoutine(bool isInstantDead = false)
        {
            m_spriteRenderer.enabled = false;
            m_collider.enabled = false;
            yield return new WaitForSeconds(m_delayBeforeDeath);
            gameObject.SetActive(false);
        }
    }
}

