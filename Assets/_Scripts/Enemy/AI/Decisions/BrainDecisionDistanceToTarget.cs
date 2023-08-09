using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainDecisionDistanceToTarget : BrainDecision
    {
        [SerializeField] private float m_minDistance;
        [SerializeField] private float m_maxDistance;

        private float m_distance;

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_distance = Random.Range(m_minDistance, m_maxDistance);
        }

        public override bool CheckDecision()
        {
            if (m_brain.Target == null)
            {
                return false;
            }

            var curDistance = Vector2.Distance(m_brain.Target.position, m_brain.transform.parent.position);
            return curDistance >= m_distance;
        }
    }
}

