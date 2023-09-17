using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Dash")]
    public class DashPowerUp : PowerUpData
    {
        [Header("Dash parameter")] 
        [SerializeField] private float m_dashLerpSpeed;
        [SerializeField] private float m_dashCooldown;
        [SerializeField] private float m_dashDistance;

        public float LerpSpeed => m_dashLerpSpeed;
        public float CoolDown => m_dashCooldown;
        public float DashDistance => m_dashDistance;
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }

        public override void ApplyPowerUp()
        {
            base.ApplyPowerUp();
            IsActive = true;
            m_runtimeWorldSet.PowerUpManager.SetActivePowerUp(this);
        }
    }
}

