using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionsSwapToBrain : BrainAction
    {
        [SerializeField] private EnemyBrain m_brainToSwap;
        [SerializeField] private bool m_keepTarget;

        public override void DoAction()
        {
            m_brain.BrainActive = false;
            m_brain.ResetBrain();
            m_brain.gameObject.SetActive(false);

            if (m_keepTarget)
            {
                m_brainToSwap.Target = m_brain.Target;
            }
           
            m_brainToSwap.gameObject.SetActive(true);
            m_brainToSwap.BrainActive = true;
        }
    }
}

