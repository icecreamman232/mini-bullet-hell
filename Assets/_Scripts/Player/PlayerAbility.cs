using UnityEngine;

namespace JustGame.Scripts.Player
{
    /// <summary>
    /// Base class for player ability
    /// </summary>
    public class PlayerAbility : MonoBehaviour
    {
        public bool IsPermit = true;
        
        protected virtual void Update()
        {
            if (!IsPermit) return;
            
            HandleInput();
            ProcessAbility();
        }

        public virtual void Initialize()
        {
            
        }
        
        protected virtual void HandleInput()
        {
            
        }

        protected virtual void ProcessAbility()
        {
            
        }

        protected virtual void ResetAbility()
        {
            
        }
    }
}

