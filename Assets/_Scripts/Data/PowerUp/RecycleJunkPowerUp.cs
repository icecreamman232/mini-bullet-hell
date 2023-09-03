using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Recycle Junk")]
    public class RecycleJunkPowerUp : PowerUpData
    {
        /// <summary>
        /// Ex: 5 mean 5 debris for 1 health point
        /// </summary>
        [SerializeField] private float ConvertRateFromDebrisToHealth;

        public float GetHealthValue(int amountDebris)
        {
            return amountDebris / ConvertRateFromDebrisToHealth;
        }
        
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

