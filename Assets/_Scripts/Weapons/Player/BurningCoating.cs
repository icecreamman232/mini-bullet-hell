using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class BurningCoating : MonoBehaviour
    {
        [SerializeField] private BurningCoatingPowerUp m_burningCoatingPowerUp;
        [SerializeField] private DamageHandler m_damageHandler;

        private void Start()
        {
            m_damageHandler.OnHit += OnHitTarget;
        }

        private void OnHitTarget(GameObject target)
        {
            if (!m_burningCoatingPowerUp.IsActive) return;
            if (!m_burningCoatingPowerUp.CanBurn) return;

            var burning = target.GetComponentInParent<CanBurn>();
            burning.ApplyBurn();
        }
        
        private void OnDestroy()
        {
            m_damageHandler.OnHit -= OnHitTarget;
        }
    }
}

