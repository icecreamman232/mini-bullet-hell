using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Flash Protector")]
    public class FlashProtectorPowerUp : PowerUpData
    {
        [SerializeField] private float m_flashReducePercent;
        public float FlashReducePercent => m_flashReducePercent;
        
        
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
    }
}
