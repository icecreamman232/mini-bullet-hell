using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    /// <summary>
    /// Set player as brain target
    /// </summary>
    public class BrainActionSetTargetPlayer : BrainAction
    {
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        
        public override void DoAction()
        {
            if (m_playerComponentSet.Player == null)
            {
                return;
            }
            m_brain.Target = m_playerComponentSet.Player.transform;
        }
    }
}

