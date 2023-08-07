using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainDecisionIsPlayerAlive : BrainDecision
    {
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        public override bool CheckDecision()
        {
            return m_playerComponentSet.Health.IsDead;
        }
    }
}

