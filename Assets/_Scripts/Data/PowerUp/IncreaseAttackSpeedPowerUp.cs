using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Increase Atk Speed")]
    public class IncreaseAttackSpeedPowerUp : PowerUpData
    {
        [SerializeField] private float m_atkSpeedIncreasePerTime;

        public float AtkSpeedIncreasePerTime => m_atkSpeedIncreasePerTime;
        
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

