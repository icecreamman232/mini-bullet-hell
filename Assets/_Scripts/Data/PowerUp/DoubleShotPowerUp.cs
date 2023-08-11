using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Double Shot")]
    public class DoubleShotPowerUp : PowerUpData
    {
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

