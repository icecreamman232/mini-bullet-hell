using System;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyXP : MonoBehaviour
    {
        [SerializeField] private int m_xpDrop;
        [SerializeField] private IntEvent m_gainXPEvent;
        [SerializeField] private EnemyHealth m_health;

        private void Start()
        {
            m_health.OnDeath += DropXPOnDeath;
        }

        private void DropXPOnDeath()
        {
            m_gainXPEvent.Raise(m_xpDrop);
        }

        private void OnDestroy()
        {
            m_health.OnDeath -= DropXPOnDeath;
        }
    }
}

