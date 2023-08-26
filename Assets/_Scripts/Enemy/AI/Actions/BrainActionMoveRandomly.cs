using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionMoveRandomly : BrainAction
    {
        [SerializeField] private EnemyMovement m_movement;

        public override void DoAction()
        {
            var randomDir = Random.insideUnitCircle;
            
            m_movement.SetDirection(randomDir);
        }
    }
}

