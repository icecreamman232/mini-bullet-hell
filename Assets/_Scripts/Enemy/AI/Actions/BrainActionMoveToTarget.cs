using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionMoveToTarget : BrainAction
    {
        [SerializeField] private bool m_stopMovingOnExitState;
        private EnemyMovement m_movement;
        private Vector2 m_curDirection;
        
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_movement = m_brain.GetComponentInParent<EnemyMovement>();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_movement.StartMoving();
        }

        public override void DoAction()
        {
            if (m_brain.Target == null) return;
            m_curDirection = (m_brain.Target.position - m_brain.transform.parent.position).normalized;
            m_movement.SetDirection(m_curDirection);
        }

        public override void OnExitState()
        {
            if (m_stopMovingOnExitState)
            {
                m_movement.StopMoving();
            }
            base.OnExitState();
        }
    }
}

