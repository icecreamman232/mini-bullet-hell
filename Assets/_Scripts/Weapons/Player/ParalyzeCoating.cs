using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class ParalyzeCoating : MonoBehaviour
    {
        [SerializeField] private ParalyzeCoatingPowerUp m_paralyzeCoatingPowerUp;
        [SerializeField] private DamageHandler m_damageHandler;
        public bool IsActive;

        private void Start()
        {
            m_damageHandler.OnHit += OnHitTarget;
        }

        private void OnHitTarget(GameObject target)
        {
            if (!IsActive) return;
            var canParalyze = target.GetComponent<CanParalyze>();
            if (canParalyze != null)
            {
                canParalyze.TriggerParalyze(m_paralyzeCoatingPowerUp.FreezeDuration, m_paralyzeCoatingPowerUp.CoatingColor);
            }
        }

        private void OnDestroy()
        {
            m_damageHandler.OnHit -= OnHitTarget;
        }
    }
}
