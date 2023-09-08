using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Revive")]
    public class RevivePowerUp : PowerUpData
    {
        [SerializeField] private bool m_hasRevived;
        [SerializeField] private GameObject m_ankhVFXPrefab;
        public bool HasRevived => m_hasRevived;
        public GameObject AnkhVFXPrefab => m_ankhVFXPrefab;

        public Action OnPlayVFX;
        
        public bool IsVFXDone { get; set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_hasRevived = false;
            IsVFXDone = false;
        }

        public void SetReviveDone()
        {
            m_hasRevived = true;
        }
        
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }

        public override void ApplyPowerUp()
        {
            base.ApplyPowerUp();
            IsActive = true;
        }

        public void TriggerRevive()
        {
            OnPlayVFX?.Invoke();
        }
    }
}

