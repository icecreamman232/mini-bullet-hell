using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Paralyze coating")]
    public class ParalyzeCoatingPowerUp : PowerUpData
    {
        [SerializeField] private Color m_coatingColor;
        [SerializeField] private float m_chance;
        [SerializeField] private float m_freezeDuration;

        public Color CoatingColor => m_coatingColor;
        public float FreezeDuration => m_freezeDuration;
        
        public bool CanParalyze()
        {
            var random = Random.Range(0f, 100f);
            return random <= m_chance;
        }

        public override void ApplyPowerUp()
        {
            base.ApplyPowerUp();
            IsActive = true;
        }

        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

