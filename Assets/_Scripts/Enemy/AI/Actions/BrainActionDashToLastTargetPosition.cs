
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionDashToLastTargetPosition : BrainAction
    {
        [SerializeField] private EnemyDash m_enemyDash;
        
        public override void DoAction()
        {
            var destination = m_brain.Target.position;
            m_enemyDash.StartDash(destination);
        }
    }
 
}
