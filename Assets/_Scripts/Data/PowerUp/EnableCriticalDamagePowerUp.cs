using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Enable critical damage")]
    public class EnableCriticalDamagePowerUp : PowerUpData
    {
        [Header("Critical values")]
        [SerializeField] private float m_averageCritMultiplier;
        [SerializeField] private float m_greatCritMultiplier;
        [SerializeField] private float m_critChance;
        [SerializeField] private float m_greatCritChance;

        [Header("Color values")] 
        [SerializeField] private Color m_averageColor;
        [SerializeField] private Color m_greatColor;
        
        public float AverageCritMultiplier => m_averageCritMultiplier;
        public float GreatCritMultiplier => m_averageCritMultiplier;
        public float CritChance => m_critChance;
        public float GreatCritChance => m_critChance;

        public Color AverageColor => m_averageColor;
        public Color GreatColor => m_greatColor;
        

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

