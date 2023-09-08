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

        public List<PowerUpData> GetPowerUpList()
        {
            var newList = new List<PowerUpData>();
            newList.Add(GetPowerUp());
            newList.Add(GetPowerUp());
            newList.Add(GetPowerUp());
            return newList;
        }

        public void RemovePowerUp(PowerUpData powerUp)
        {
            for (int i = 0; i < m_powerUpList.Length; i++)
            {
                if(m_powerUpList[i] == null) continue;
                if (m_powerUpList[i].Name == powerUp.Name)
                {
                    m_powerUpList[i] = null;
                }
            }
        }
        
        public PowerUpData GetPowerUp()
        {
            PowerUpData powerup;
            do
            {
                powerup = m_powerUpList[Random.Range(0, m_powerUpList.Length)];
            } while (powerup == null);

            return powerup;
        }
    }
}

