using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Items
{
    public class Derbis : Item
    {
        [SerializeField] private float m_liveDuration;
        [SerializeField] private ResourceEvent m_resourceEvent;
        [SerializeField] private AutoCollectPowerUp m_autoCollectPowerUp;
        
        private void OnEnable()
        {
            Invoke(nameof(OnSetDestroy),m_liveDuration);
            if (m_autoCollectPowerUp.IsActive)
            {
                Invoke(nameof(AutoCollect),m_autoCollectPowerUp.DelayBeforeAutoCollect);
            }
        }

        private void AutoCollect()
        {
            m_canMove = true;
        }
        
        protected override void OnSetDestroy()
        {
            m_resourceEvent.AddDerbis(1);
            base.OnSetDestroy();
        }
    }
}

