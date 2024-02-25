using System;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Scripts.Enemy
{
    public class EnemyXP : MonoBehaviour
    {
        [SerializeField] private int m_baseXP;
        [SerializeField][ReadOnly] private int m_curXPDrop;
        [SerializeField] private IntEvent m_gainXPEvent;
        [SerializeField] private EnemyHealth m_health;

        private void Start()
        {
            m_health.OnDeath += DropXPOnDeath;
        }

        private void DropXPOnDeath()
        {
            m_gainXPEvent.Raise(m_baseXP);
        }

        private void OnDestroy()
        {
            m_health.OnDeath -= DropXPOnDeath;
        }
    }
}

