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
        [SerializeField] private ShipAttribute attribute;
        [SerializeField] private AnimationParameter m_deadAnim;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private PlayerComponentSet m_componentSet;
        [SerializeField] protected FloatEvent m_healthEvent;
        [SerializeField] protected ResourceEvent m_resourceEvent;
        [Header("Powerups")]
        [SerializeField] protected HealingPowerUp m_healingPowerUp;
        [SerializeField] protected RecycleJunkPowerUp m_recycleJunkPowerUp;
        [SerializeField] protected SacrificeHPPowerUp m_sacrificeHpPowerUp;
        [SerializeField] protected RevivePowerUp m_revivePowerUp;
        [SerializeField] protected MadnessPowerUp m_madnessPowerUp;
        [Header("Shaking")]
        [SerializeField] private ScreenShakeEvent m_shakeEvent;
        [SerializeField] private ShakeProfile m_shakeProfile;
        [Header("SFX")] 
        [SerializeField] private PlaySoundFX m_hitSFX;

        private float m_regenTimer;
        private bool m_isReviving;
        private void Awake()
        {
            m_componentSet.SetHealth(this);
        }

        protected override void Start()
        {
            base.Start();
            m_resourceEvent.OnCollectDerbis += HealingUpByRecycleJunk;
            m_healingPowerUp.OnApplyPowerUp += HealingUp;
            m_sacrificeHpPowerUp.OnApplyPowerUp += SacrificeHP;
            m_madnessPowerUp.OnApplyPowerUp += OnTriggerMadness;
        }

        protected override void Initialize()
        {
            m_maxHealth = attribute.BaseHealth;
            base.Initialize();
        }

        protected override void Update()
        {
            if (attribute.BaseHPRegeneration > 0)
            {
                m_regenTimer += Time.deltaTime;
                if (m_regenTimer >= 1)
                {
                    ComputeHealthRegeneration();
                    m_regenTimer = 0;
                }
            }
            base.Update();
        }

        private void ComputeHealthRegeneration()
        {
            m_curHealth += attribute.BaseHPRegeneration;
            m_curHealth = Mathf.Clamp(m_curHealth, 0, m_maxHealth);
            UpdateUI();
        }
        

        public void SetMaxHealth(float value)
        {
            m_maxHealth = value;
            if (m_curHealth > m_maxHealth)
            {
                m_curHealth = m_maxHealth;
            }
        }

        private void SacrificeHP()
        {
            SetMaxHealth(m_maxHealth - m_maxHealth * m_sacrificeHpPowerUp.HPPercentReduce);
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
            m_resourceEvent.SpendDerbis(debrisAmount);
            m_curHealth += healingAmount;
            m_curHealth = Mathf.Clamp(m_curHealth, 0f, m_maxHealth);
            if (m_healthEvent != null)
            {
                m_healthEvent.Raise(m_curHealth/m_maxHealth);
            }
        }

        private void OnTriggerMadness()
        {
            m_madnessPowerUp.CheckHPLost((m_maxHealth - m_curHealth) / m_maxHealth * 100);
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Test Damage")]
        private void TestDamage()
        {
            TakeDamage(15, null);
        }
        
        #endif

        private const float m_armorFactor = 0.05f;
        protected override float ComputeFinalDamage(float rawDamage)
        {
            var reducePercent = 1 - (attribute.BaseArmor * m_armorFactor) / (1 + attribute.BaseArmor * m_armorFactor);
            var finalDamage = rawDamage * reducePercent;
            finalDamage = Mathf.Round(finalDamage);
            return finalDamage;
        }

        public override void TakeDamage(float damage, GameObject instigator, bool isCriticalHit = false, bool isInstantDead = false)
        {
            if (!AuthorizeTakingDamage()) return;
            
            m_curHealth -= ComputeFinalDamage(damage);
            
            if (m_hitSFX != null)
            {
                m_hitSFX.PlaySFX();
            }
            
            OnHit?.Invoke();

            if (m_madnessPowerUp.IsActive)
            {
                m_madnessPowerUp.CheckHPLost((m_maxHealth - m_curHealth) / m_maxHealth * 100);
            }
            
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

            ProcessKill(isInstantDead);
        }

        protected override void ProcessKill(bool isInstantDead = false)
        {
            if (m_revivePowerUp.IsActive && !m_revivePowerUp.HasRevived)
            {
                StartCoroutine(OnRevive());
                return;
            }
            base.ProcessKill(isInstantDead);
        }

        private IEnumerator OnRevive()
        {
            if (m_isReviving)
            {
                yield break;
            }

            m_isReviving = true;
            m_revivePowerUp.TriggerRevive();
            yield return new WaitUntil(() => m_revivePowerUp.IsVFXDone);
            m_curHealth = m_maxHealth;
            m_revivePowerUp.SetReviveDone();
            UpdateUI();
            m_isReviving = false;
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

        protected override IEnumerator KillRoutine(bool isInstantDead = false)
        {
            m_isDead = true;
            m_collider.enabled = false;
            m_deadAnim.SetTrigger();
            yield return new WaitForSeconds(m_delayBeforeDeath);
            gameObject.SetActive(false);
            m_gameCoreEvent.SetGameState(GameState.GAME_OVER);
        }

        private void OnDestroy()
        {
            m_resourceEvent.OnCollectDerbis -= HealingUpByRecycleJunk;
            m_healingPowerUp.OnApplyPowerUp -= HealingUp;
            m_sacrificeHpPowerUp.OnApplyPowerUp -= SacrificeHP;
            m_madnessPowerUp.OnApplyPowerUp -= OnTriggerMadness;
        }
    } 
}

