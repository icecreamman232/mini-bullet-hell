using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyFlashExplosion : MonoBehaviour
    {
        [SerializeField] private float m_flashDuration;
        [SerializeField] private FloatEvent m_flashScreenEvent;
        private Health m_health;

        private void Start()
        {
            m_health = GetComponent<Health>();
            m_health.OnDeath += TriggerFlashScreen;
        }

        private void TriggerFlashScreen()
        {
            m_flashScreenEvent.Raise(m_flashDuration);
        }
    }
}

