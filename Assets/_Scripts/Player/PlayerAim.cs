using System;
using JustGame.Scripts.Managers;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerAim : PlayerAbility
    {
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private float m_offsetAngle;
        [SerializeField] private Transform m_transformToRotate;
        
        private InputManager m_inputManager;
        private Vector2 m_aimDirection;
        private float m_angle;

        public Action<Quaternion> RotateCallback;
        
        public Vector2 AimDirection => m_aimDirection;
        public override void Initialize()
        {
            base.Initialize();
            m_inputManager = InputManager.Instance;
        }

        protected override void HandleInput()
        {
            if (m_runtimeWorldSet.GameManager.IsPaused) return;
            
            m_aimDirection = (m_inputManager.GetWorldMousePos() - transform.position).normalized;
            m_angle = Mathf.Atan2(m_aimDirection.y, m_aimDirection.x) * Mathf.Rad2Deg + m_offsetAngle;
            
            m_transformToRotate.rotation = Quaternion.AngleAxis(m_angle,Vector3.forward);
            
            RotateCallback?.Invoke(Quaternion.AngleAxis(m_angle,Vector3.forward));
            
            base.HandleInput();
        }
    }
}

