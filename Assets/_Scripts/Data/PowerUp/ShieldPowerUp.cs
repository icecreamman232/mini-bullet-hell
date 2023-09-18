using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Shield")]
    public class ShieldPowerUp : PowerUpData
    {
        [Header("Paramemter")]
        [SerializeField] private float m_Cooldown;

        [SerializeField] private int m_maxShieldPoint;

        public int MaxShield => m_maxShieldPoint;
        public float Cooldown => m_Cooldown;
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }

        public override void ApplyPowerUp()
        {
            base.ApplyPowerUp();
            IsActive = true;
            m_runtimeWorldSet.PowerUpManager.SetActivePowerUp(this);
        }

        public override void DiscardPowerUp()
        {
            IsActive = false;
            base.DiscardPowerUp();
        }
    }
}
