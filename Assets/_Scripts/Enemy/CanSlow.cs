using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class CanSlow : MonoBehaviour
    {
        [SerializeField] private EnemyMovement m_enemyMovement;
        [SerializeField] private SlowCoatingPowerUp m_slowCoatingPowerUp;
        private float m_timer;
        
        public void ApplySlow()
        {
            m_timer = m_slowCoatingPowerUp.Duration;
            m_enemyMovement.SetOverrideSpeed(m_enemyMovement.CurrentSpeed - m_slowCoatingPowerUp.SlowReduce);
        }

        private void RemoveSlow()
        {
            m_enemyMovement.ResetSpeed();
        }

        private void Update()
        {
            if (m_timer <= 0) return;

            m_timer -= Time.deltaTime;
            if (m_timer <= 0)
            {
                RemoveSlow();
            }
        }
    }
}

