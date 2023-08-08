using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyKillScore : MonoBehaviour
    {
        [SerializeField] private int m_killScore;
        [SerializeField] private KillScoreEvent m_killScoreEvent;
        private Health m_health;
        private void Start()
        {
            m_health = GetComponent<Health>();
            m_health.OnDeath += AddScore;
        }

        private void AddScore()
        {
            m_killScoreEvent.AddKillScore(m_killScore);
        }
    }
}

