using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainDecisionCheckDash : BrainDecision
    {
        [SerializeField] private EnemyDash m_enemyDash;

        private bool m_isDashDone;

        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_enemyDash.OnFinishDash += CheckDash;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_isDashDone = false;
        }

        private void CheckDash()
        {
            m_isDashDone = true;
        }
        
        public override bool CheckDecision()
        {
            return m_isDashDone;
        }
    }

}
