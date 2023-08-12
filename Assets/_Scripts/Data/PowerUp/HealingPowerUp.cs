using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Heal Up")]
    public class HealingPowerUp : PowerUpData
    {
        public float HealingUpPercent;
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

