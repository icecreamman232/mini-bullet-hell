using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Increase Range")]
    public class IncreaseRangePowerUp : PowerUpData
    {
        [SerializeField] private float m_rangeIncreasePerPowerUp;

        public float RangeIncreasePerTime => m_rangeIncreasePerPowerUp;
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }
    }  
}

