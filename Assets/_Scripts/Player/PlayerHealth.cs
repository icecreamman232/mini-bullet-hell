using System;
using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class PlayerHealth : Health
    {
        [SerializeField] private bool m_immuneDamage;
        [SerializeField] private AnimationParameter m_deadAnim;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private PlayerComponentSet m_componentSet;
        [SerializeField] protected FloatEvent m_healthEvent;
        [SerializeField] protected ResourceEvent m_resourceEvent;
        [SerializeField] protected HealingPowerUp m_healingPowerUp;
        [SerializeField] protected RecycleJunkPowerUp m_recycleJunkPowerUp;
        [Header("Shaking")]
        [SerializeField] private ScreenShakeEvent m_shakeEvent;
        [SerializeField] private ShakeProfile m_shakeProfile;
        [Header("SFX")] 
        [SerializeField] private PlaySoundFX m_hitSFX;
        private void Awake()
        {
            m_componentSet.SetHealth(this);
        }

        protected override void Start()
        {
            base.Start();
            m_resourceEvent.OnCollectDerbis += HealingUpByRecycleJunk;
            m_healingPowerUp.OnApplyPowerUp += HealingUp;
        }

        public void SetMaxHealth(float value)
        {
            m_maxHealth = value;
        }

        private void HealingUp()
        {
            m_curHealth += MathHelpers.Percent(m_maxHealth, m_healingPowerUp.HealingUpPercent);
            if (m_healthEvent != null)
            {
                m_healthEvent.Raise(m_curHealth/m_maxHealth);
            }
        }

        private void HealingUpByRecycleJunk(int debrisAmount)
        {
            if (!m_recycleJunkPowerUp.IsActive) return;
            if (m_curHealth >= m_maxHealth) return;
            
            float healingAmount = m_recycleJunkPowerUp.GetHealthValue(debrisAmount); 
            Debug.Log($"Heal amount {healingAmount}");
            m_curHealth += healingAmount;
            m_curHealth = Mathf.Clamp(m_curHealth, 0f, m_maxHealth);
            if (m_healthEvent != null)
            {
                m_healthEvent.Raise(m_curHealth/m_maxHealth);
            }
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Test Damage")]
        private void TestDamage()
        {
            TakeDamage(15, null);
        }
        
        #endif
        
        public override void TakeDamage(float damage, GameObject instigator)
        {
            if (!AuthorizeTakingDamage()) return;

            m_curHealth -= damage;
            
            if (m_hitSFX != null)
            {
                m_hitSFX.PlaySFX();
            }
            
            OnHit?.Invoke();
            
            m_shakeEvent.DoShake(m_shakeProfile);
            
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

        protected override void UpdateUI()
        {
            if (m_healthEvent != null)
            {
                m_healthEvent.Raise(m_curHealth/m_maxHealth);
            }
            base.UpdateUI();
        }

        protected override bool AuthorizeTakingDamage()
        {
            if (m_immuneDamage)
            {
                return false;
            }

            if (m_isDead)
            {
                return false;
            }
            
            if (m_isInvulnerable)
            {
                return false;
            }
            return true;
        }

        protected override IEnumerator KillRoutine()
        {
            m_isDead = true;
            m_collider.enabled = false;
            m_deadAnim.SetTrigger();
            yield return new WaitForSeconds(m_delayBeforeDeath);
            gameObject.SetActive(false);
            m_gameCoreEvent.SetGameState(GameState.GAME_OVER);
        }
    } 
}

