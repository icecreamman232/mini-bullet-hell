using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class EnemyHealth : Health
    {
        [SerializeField] private WaveEvent m_waveEvent;
        [SerializeField] private AnimationParameter m_explodeAnim;
        [SerializeField] private PlaySoundFX m_hitSFX;
        [SerializeField] ObjectPooler m_dmgNumberPooler;
        [SerializeField] private bool m_immuneDamage;
        private Loot m_loot;

        protected override void Initialize()
        {
            base.Initialize();
            m_loot = GetComponent<Loot>();
            if (m_hitSFX != null)
            {
                OnHit += m_hitSFX.PlaySFX;
            }
        }

        private void OnEnable()
        {
            if (m_waveEvent.CurrentWave <= 1) return;
            m_maxHealth += (m_waveEvent.CurrentWave * MathHelpers.PercentOf(m_maxHealth) * 2); // Increase 2% HP per wave
        }

        public void EnableDamageImmune()
        {
            m_immuneDamage = true;
        }
        
        public void DisableDamageImmune()
        {
            m_immuneDamage = false;
        }

        public override void TakeDamage(float damage, GameObject instigator)
        {
            if (!AuthorizeTakingDamage()) return;

            m_curHealth -= damage;
            
            if (m_dmgNumberPooler == null)
            {
                Debug.LogError($"Damage number pooler not found on {this.gameObject.name}");
            }
            
            var dmgNumber = m_dmgNumberPooler.GetPooledGameObject().GetComponent<DamageNumber>();
            dmgNumber.Show(Mathf.RoundToInt(damage));
            
            OnHit?.Invoke();

            UpdateUI();

            if (m_curHealth >= 0)
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

