using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerDash : PlayerAbility
    {
        [SerializeField] private DashPowerUp m_dashPowerUp;
        [SerializeField] private PlayerAim m_playerAim;
        private bool m_isDashing;
        private bool m_isCooldown;
        private float m_timer;
        private Vector2 m_dashTarget;
        private InputManager m_inputManager;

        public override void Initialize()
        {
            base.Initialize();
            m_inputManager = InputManager.Instance;
        }

        protected override void ProcessAbility()
        {
            if (m_isCooldown)
            {
                m_timer -= Time.deltaTime;
                if (m_timer <= 0)
                {
                    m_isCooldown = false;
                    m_timer = 0;
                }
            }
            base.ProcessAbility();
        }

        protected override void HandleInput()
        {
            if (!m_dashPowerUp.IsActive) return;
            if (m_isDashing) return;
            if (m_isCooldown) return;
            
            if (m_inputManager.GetKeyClicked(BindingAction.USE_ACTIVE_ABILITY))
            {
                StartCoroutine(DashRoutine());
            }
            
            base.HandleInput();
        }

        private IEnumerator DashRoutine()
        {
            m_isDashing = true;
            var initPos = (Vector2)transform.parent.position;
            m_dashTarget = initPos + m_playerAim.AimDirection * m_dashPowerUp.DashDistance;
            float tValue = 0;
            
            while (tValue < 1)
            {
                transform.parent.position = Vector2.Lerp(initPos, m_dashTarget, tValue);
                tValue += m_dashPowerUp.LerpSpeed;
                yield return null;
            }
            
            //Start cooldown
            m_isCooldown = true;
            m_timer = m_dashPowerUp.CoolDown;
            m_isDashing = false;
        }
    }
}

