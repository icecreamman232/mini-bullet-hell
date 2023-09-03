using System.Collections.Generic;
using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Managers
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private PowerUpData[] m_powerUpList;

        private void Start()
        {
            m_runtimeWorldSet.SetPowerUpManager(this);
        }

        public List<PowerUpData> GetPowerUps()
        {
            var powerUpIndex1 = Random.Range(0, m_powerUpList.Length);
            var powerUpIndex2 = Random.Range(0, m_powerUpList.Length);
            var powerUpIndex3 = Random.Range(0, m_powerUpList.Length);

            var newList = new List<PowerUpData>();
            newList.Add(m_powerUpList[powerUpIndex1]);
            newList.Add(m_powerUpList[powerUpIndex2]);
            newList.Add(m_powerUpList[powerUpIndex3]);
            return newList;
        }
    }
}

