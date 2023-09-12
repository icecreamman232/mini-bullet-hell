using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    /*
     * The more HP lost, the more damage player will gain
     */
    
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Madness")]
    public class MadnessPowerUp : PowerUpData
    {
        [SerializeField] private float m_percentDamageIncrease;
        [SerializeField] private float m_percentHPLost;
        private float m_damageIncrease;

        public float DamageIncreasePercent => m_damageIncrease;

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

        public void CheckHPLost(float hpPercentLost)
        {
            var rate = hpPercentLost / m_percentHPLost;
            Debug.Log($"Rate {rate}");
            m_damageIncrease = m_percentDamageIncrease * rate;
            Debug.Log($"Damage increase {m_damageIncrease}");
        }
    }
}
