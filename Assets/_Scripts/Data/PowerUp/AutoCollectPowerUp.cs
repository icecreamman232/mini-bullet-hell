using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Auto Collect")]
    public class AutoCollectPowerUp : PowerUpData
    {
        [SerializeField] private float m_delayBeforeAutoCollect;

        public float DelayBeforeAutoCollect => m_delayBeforeAutoCollect;
        
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
