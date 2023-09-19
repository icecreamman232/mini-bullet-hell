using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Burning coating")]
    public class BurningCoatingPowerUp : PowerUpData
    {
        [SerializeField] private int m_minDamage;
        [SerializeField] private int m_maxDamage;
        [SerializeField] private float m_duration;
        [SerializeField] private float m_damageFrequency;
        [SerializeField] private float m_chanceToApply;
        [SerializeField] private float m_chanceToSpreadOut;
        public float DamageFrequency => m_damageFrequency;
        public float ChanceToSpreadOut => m_chanceToSpreadOut;
        public float ChanceToApply => m_chanceToApply;
        public float Duration => m_duration;
        public int BurnDamage => Random.Range(m_minDamage, m_maxDamage);

        public bool CanBurn =>  Random.Range(0f, 100f) <= m_chanceToApply;

        public bool CanSpread => Random.Range(0f, 100f) <= m_chanceToSpreadOut;
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
            IsActive = true;
        }

        public override void ApplyPowerUp()
        {
            base.ApplyPowerUp();
            IsActive = true;
        }
    }
}

