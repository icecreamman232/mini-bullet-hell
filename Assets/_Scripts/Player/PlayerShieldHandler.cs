using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerShieldHandler : PlayerAbility
    {
        [SerializeField] private ShieldPowerUp m_shieldPowerUp;
        [SerializeField] private IntEvent m_removeShieldBarUIEvent;
        [SerializeField] private CooldownActivePowerUpEvent m_cooldownActivePowerUpEvent;
        [SerializeField] private PlayerShield m_shield;
        [SerializeField] private Collider2D m_playerCollider;
        private int m_maxShieldPoint;
        private int m_curShieldPoint;
        private bool m_isCooldown;
        private float m_timer;
        private void Awake()
        {
            m_shieldPowerUp.OnApplyPowerUp += ApplyShield;
            m_shieldPowerUp.OnDiscardPowerUp += DiscardShield;
            m_shield.OnShieldHit += OnShieldHit;
            
        }

        protected override void ProcessAbility()
        {
            if (!m_isCooldown) return;
            m_timer -= Time.deltaTime;
            m_cooldownActivePowerUpEvent.Raise(m_timer);
            if (m_timer <= 0)
            {
                m_timer = 0;
                m_isCooldown = false;
                m_cooldownActivePowerUpEvent.Raise(m_timer);
                EnableShield();
            }
            
            base.ProcessAbility();
        }

        private void ApplyShield()
        {
            m_shield.gameObject.SetActive(true);
            m_maxShieldPoint = m_shieldPowerUp.MaxShield;
            m_curShieldPoint = m_maxShieldPoint;
        }

        private void DiscardShield()
        {
            m_playerCollider.enabled = true;
            m_shield.gameObject.SetActive(false);
        }
        
        private void OnShieldHit()
        {
            m_curShieldPoint--;
            m_removeShieldBarUIEvent.Raise(m_curShieldPoint);
            if (m_curShieldPoint <= 0)
            {
                DisableShield();
            }
        }

        private void EnableShield()
        {
            m_playerCollider.enabled = false;
            m_curShieldPoint = m_maxShieldPoint;
            m_shield.gameObject.SetActive(true);
        }
        
        private void DisableShield()
        {
            m_playerCollider.enabled = true;
            m_shield.gameObject.SetActive(false);
            m_timer = m_shieldPowerUp.Cooldown;
            m_cooldownActivePowerUpEvent.SetCoolDown(m_shieldPowerUp.Cooldown);
            m_isCooldown = true;
        }
        
        private void OnDestroy()
        {
            m_shieldPowerUp.OnApplyPowerUp -= ApplyShield;
            m_shieldPowerUp.OnDiscardPowerUp -= DiscardShield;
            m_shield.OnShieldHit -= OnShieldHit;
        }
    }
}
