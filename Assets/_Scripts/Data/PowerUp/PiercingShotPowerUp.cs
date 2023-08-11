using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Piercing Shot")]
    public class PiercingShotPowerUp : PowerUpData
    {
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }
}

