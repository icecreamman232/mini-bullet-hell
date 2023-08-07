using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public abstract class BrainAction : MonoBehaviour
    {
        public string Label;
        protected EnemyBrain m_brain;
        
        public virtual void Initialize(EnemyBrain brain)
        {
            m_brain = brain;
        }
        public virtual void OnEnterState(){}
        public abstract void DoAction();
        public virtual void OnExitState(){}
    }
}

