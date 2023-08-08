using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionPatrolWithinCircle : BrainAction
    {
        [SerializeField] private float m_radius;
        private EnemyMovement m_movement;
        private Vector2 m_initPos;
        private Vector2 m_nextPoint;
        
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_initPos = m_brain.transform.parent.position;
            m_movement = m_brain.GetComponentInParent<EnemyMovement>();
            m_nextPoint = Random.insideUnitCircle * m_radius + m_initPos;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_nextPoint = Random.insideUnitCircle * m_radius + m_initPos;
            m_movement.SetTarget(m_nextPoint);
        }

        public override void DoAction()
        {
            var distance = Vector2.Distance(m_nextPoint, m_brain.transform.parent.position);
            if (distance <= 0.1f)
            {
                m_nextPoint = Random.insideUnitCircle * m_radius + m_initPos;
                m_movement.SetTarget(m_nextPoint);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(
                m_initPos != Vector2.zero 
                    ? m_initPos
                    : transform.position, m_radius);
        }
    }
}

