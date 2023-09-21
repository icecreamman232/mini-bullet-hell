using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Slow Coating")]
    public class SlowCoatingPowerUp : PowerUpData
    {
        [SerializeField] private float m_slowReduce;
        [SerializeField] private float m_duration;
        [SerializeField] private float m_chanceToApply;

        public float SlowReduce => m_slowReduce;
        public float Duration => m_duration;
        public float ChanceToApply => m_chanceToApply;

        public bool CanSlow => Random.Range(0f, 100f) <= m_chanceToApply;
        
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

