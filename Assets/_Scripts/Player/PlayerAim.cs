using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerAim : PlayerAbility
    {
        [SerializeField] private float m_offsetAngle;
        [SerializeField] private Transform m_transformToRotate;
        
        private InputManager m_inputManager;
        private Vector2 m_aimDirection;
        private float m_angle;
        public override void Initialize()
        {
            base.Initialize();
            m_inputManager = InputManager.Instance;
        }

        protected override void HandleInput()
        {
            m_aimDirection = (m_inputManager.GetWorldMousePos() - transform.position).normalized;
            m_angle = Mathf.Atan2(m_aimDirection.y, m_aimDirection.x) * Mathf.Rad2Deg + m_offsetAngle;
            
            m_transformToRotate.rotation = Quaternion.AngleAxis(m_angle,Vector3.forward);
            
            base.HandleInput();
        }
    }
}

