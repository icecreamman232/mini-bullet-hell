using System;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.UI;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class SlowCoating : MonoBehaviour
    {
        [SerializeField] private SlowCoatingPowerUp m_slowCoatingPowerUp;
        [SerializeField] private DamageHandler m_damageHandler;

        private void Start()
        {
            m_damageHandler.OnHit += OnHitTarget;
        }

        private void OnHitTarget(GameObject target)
        {
            if (!m_slowCoatingPowerUp.IsActive) return;
            if (!m_slowCoatingPowerUp.CanSlow) return;

            var slow = target.GetComponentInParent<CanSlow>();
            slow.ApplySlow();
        }

        private void OnDestroy()
        {
            m_damageHandler.OnHit -= OnHitTarget;
        }
    }
}
