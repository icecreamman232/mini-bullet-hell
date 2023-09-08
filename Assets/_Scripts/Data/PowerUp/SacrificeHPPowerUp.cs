using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Sacrifice HP")]
    public class SacrificeHPPowerUp : PowerUpData
    {
        [SerializeField] private float m_HPPercentReduce;
        [SerializeField] private float m_damageIncreasePercent;

        public float HPPercentReduce => MathHelpers.PercentOf(m_HPPercentReduce);
        public float DamageIncreasePercent => MathHelpers.PercentOf(m_damageIncreasePercent);

        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

