using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public abstract class BrainDecision : MonoBehaviour
    {
        public string Label;
        protected EnemyBrain m_brain;

        public virtual void Initialize(EnemyBrain brain)
        {
            m_brain = brain;
        }
        public virtual void OnEnterState(){}
        public abstract bool CheckDecision();
        public virtual void OnExitState(){}
    }
}

