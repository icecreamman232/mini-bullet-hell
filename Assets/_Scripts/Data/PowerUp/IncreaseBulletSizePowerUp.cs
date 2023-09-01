using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Increase Bullet Size")]
    public class IncreaseBulletSizePowerUp : PowerUpData
    {
        [Header("Config")]
        [SerializeField] private float m_sizeIncreasePerTime;
        [SerializeField] private float m_speedReducePerTime;
        [SerializeField] private float m_maxSize;
        [Header("Runtime Value")] 
        [SerializeField] private float m_curScale;
        [SerializeField] private float totalSpeedReduce;
        public float SizeIncreasePerTime => m_sizeIncreasePerTime;
        public float SpeedReducePerTime => m_speedReducePerTime;

        public float CurrentScale => m_curScale;
        public float TotalSpeedReduce => totalSpeedReduce;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_curScale = 1;
            totalSpeedReduce = 0;
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
            m_curScale += m_sizeIncreasePerTime;
            if (m_curScale > m_maxSize)
            {
                m_curScale = m_maxSize;
                return;
            }
            totalSpeedReduce += m_speedReducePerTime;
            
        }
    }
}
