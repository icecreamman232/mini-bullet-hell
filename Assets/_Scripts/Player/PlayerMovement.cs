using JustGame.Scripts.Managers;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    /// <summary>
    /// Handle movement in 8 direction of player
    /// </summary>
    public class PlayerMovement : PlayerAbility
    {
        [SerializeField] private RuntimeWorldSet m_worldSet;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;

        private InputManager m_inputManager;

        public override void Initialize()
        {
            m_inputManager = InputManager.Instance;
            base.Initialize();
        }

        protected override void HandleInput()
        {
            if (!m_inputManager.IsInputActive) return;

            if (m_inputManager.GetKeyDown(BindingAction.MOVE_LEFT))
            {
                m_movingDirection.x = -1;
            }
            else if (m_inputManager.GetKeyDown(BindingAction.MOVE_RIGHT))
            {
                m_movingDirection.x = 1;
            }
            else
            {
                m_movingDirection.x = 0;
            }
            
            if (!m_inputManager.IsInputActive) return;

            if (m_inputManager.GetKeyDown(BindingAction.MOVE_UP))
            {
                m_movingDirection.y = 1;
            }
            else if (m_inputManager.GetKeyDown(BindingAction.MOVE_DOWN))
            {
                m_movingDirection.y = -1;
            }
            else
            {
                m_movingDirection.y = 0;
            }
            
            base.HandleInput();
        }

        protected override void ProcessAbility()
        {
            Movement();
            base.ProcessAbility();
        }

        private void Movement()
        {
            transform.Translate(m_movingDirection * ((m_moveSpeed/10) * Time.deltaTime));
            if (!m_worldSet.LevelBounds.IsInBounds(transform.position))
            {
                transform.position = m_worldSet.LevelBounds.InversedPoint(transform.position);
            }
        }
    }  
}

