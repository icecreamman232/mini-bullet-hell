using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Enemy
{
    public enum ComparisonMode
    {
        LESSER,
        LESSER_AND_EQUAL,
        GREATER_AND_EQUAL,
        GREATER,
    }
    public class BrainDecisionDistanceToTarget : BrainDecision
    {
        [SerializeField] private ComparisonMode m_comparisonMode;
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

            switch (m_comparisonMode)
            {
                case ComparisonMode.LESSER:
                    return curDistance < m_distance;
                case ComparisonMode.LESSER_AND_EQUAL:
                    return curDistance <= m_distance;
                case ComparisonMode.GREATER_AND_EQUAL:
                    return curDistance >= m_distance;
                case ComparisonMode.GREATER:
                    return curDistance > m_distance;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

