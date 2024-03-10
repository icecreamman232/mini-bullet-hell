using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        [SerializeField] private ShipAttributeRuntime m_attributeRuntime;

        protected override void Start()
        {
            if (m_attributeRuntime != null)
            {
                m_minDamageCause = m_attributeRuntime.MinAtkDamage;
                m_maxDamageCause = m_attributeRuntime.MaxAtkDamage;
            }
            base.Start();
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
    }
}

