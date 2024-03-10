using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        [SerializeField] private ShipAttributeRuntime m_attributeRuntime;
        [SerializeField] private FloatEvent m_upgradeKnockBackForceEvent;
        
        protected override void Start()
        {
            if (m_attributeRuntime != null)
            {
                m_minDamageCause = m_attributeRuntime.MinAtkDamage;
                m_maxDamageCause = m_attributeRuntime.MaxAtkDamage;
            }
            
            m_upgradeKnockBackForceEvent.AddListener(OnUpgradeKnockBack);
            base.Start();
        }

        private void OnUpgradeKnockBack(float addValue)
        {
            m_knockBackForce += addValue;
        }

        protected override float GetDamage()
        {
            if (m_attributeRuntime != null)
            {
                m_minDamageCause = m_attributeRuntime.MinAtkDamage;
                m_maxDamageCause = m_attributeRuntime.MaxAtkDamage;
            }
            return base.GetDamage();
        }

        private void OnDestroy()
        {
            m_upgradeKnockBackForceEvent.RemoveListener(OnUpgradeKnockBack);
        }
    }
}

