using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.Events;

namespace JustGame.Scripts.Weapons
{
    public class EnemyHealth : Health
    {
        [SerializeField] private WaveEvent m_waveEvent;
        [SerializeField] private AnimationParameter m_explodeAnim;
        [SerializeField] ObjectPooler m_dmgNumberPooler;
        [SerializeField] private bool m_immuneDamage;
        [SerializeField] private UnityEvent m_onHitEvent;
        private Loot m_loot;
        
        protected override void Initialize()
        {
            base.Initialize();
            m_loot = GetComponent<Loot>();
        }

        private void OnEnable()
        {
            if (m_waveEvent.CurrentWave <= 1) return;
            m_maxHealth += (m_waveEvent.CurrentWave * MathHelpers.PercentOf(m_maxHealth) * 35); // Increase 50% HP per wave
        }

        public void EnableDamageImmune()
        {
            m_immuneDamage = true;
        }
        
        public void DisableDamageImmune()
        {
            m_immuneDamage = false;
        }

        public void InstantDead()
        {
            TakeDamage(m_curHealth, null);
        }
        
        public override void TakeDamage(float damage, GameObject instigator, bool isCriticalHit = false)
        {
            if (!AuthorizeTakingDamage()) return;

            m_curHealth -= damage;
            
            if (m_dmgNumberPooler == null)
            {
                Debug.LogError($"Damage number pooler not found on {this.gameObject.name}");
            }
            
            var dmgNumber = m_dmgNumberPooler.GetPooledGameObject().GetComponent<DamageNumber>();
            dmgNumber.Show(Mathf.RoundToInt(damage), isCriticalHit);
            
            OnHit?.Invoke();
            m_onHitEvent?.Invoke();
            
            UpdateUI();

            if (m_curHealth > 0)
            {
                StartCoroutine(OnInvulnerable());
                return;
            }

            ProcessKill();
        }

        protected override bool AuthorizeTakingDamage()
        {
            if (m_immuneDamage)
            {
                return false;
            }
            return true;
        }

        protected override IEnumerator KillRoutine()
        {
            m_spriteRenderer.enabled = false;
            m_collider.enabled = false;
            m_explodeAnim.SetTrigger();
            yield return new WaitForSeconds(m_explodeAnim.Duration);
            if (m_loot != null)
            {
                m_loot.SpawnLoot();
            }
            Destroy(this.gameObject);
        }
    }
}

