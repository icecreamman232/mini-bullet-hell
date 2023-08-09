using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionsRunAway : BrainAction
    {
        [SerializeField] private float m_minDistance;
        private EnemyMovement m_movement;
        private float m_distance;

        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_movement = m_brain.GetComponentInParent<EnemyMovement>();
        }

        public override void DoAction()
        {
            if (m_brain.Target == null) return;
            m_distance = Vector2.Distance(m_brain.transform.parent.position, m_brain.Target.position);
            if (m_distance <= m_minDistance)
            {
                m_movement.StartMoving();
                var dir = Vector2.zero;
                dir.x = m_brain.transform.parent.position.x < m_brain.Target.position.x ? -1 : 1;
                dir.y = m_brain.transform.parent.position.y < m_brain.Target.position.y ? -1 : 1;
                m_movement.SetDirection(dir);
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            m_movement.PauseMoving();
        }
    }
}

