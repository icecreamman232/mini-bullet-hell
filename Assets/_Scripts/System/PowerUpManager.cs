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
        [SerializeField] private PowerUpData m_curentActivePowerUp;

        public PowerUpData CurrentActivePowerUp => m_curentActivePowerUp;
        
        private void Start()
        {
            m_runtimeWorldSet.SetPowerUpManager(this);
        }

        public void SetActivePowerUp(PowerUpData newActivePowerUp)
        {
            if (m_curentActivePowerUp != null)
            {
                m_curentActivePowerUp.IsActive = false;
            }
            m_curentActivePowerUp = newActivePowerUp;
        }
        
        public List<PowerUpData> GetPowerUpList()
        {
            var newList = new List<PowerUpData>();
            var firstPowerUp = GetPowerUp(); 
            newList.Add(firstPowerUp);
            
            var secondPowerUp = GetPowerUp(firstPowerUp); 
            newList.Add(secondPowerUp);
            
            newList.Add(GetPowerUp(secondPowerUp));
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
        
        public PowerUpData GetPowerUp(PowerUpData prevPowerUp = null)
        {
            PowerUpData powerup;
            do
            {
                powerup = m_powerUpList[Random.Range(0, m_powerUpList.Length)];
                if (prevPowerUp != null)
                {
                    if (powerup.Name == prevPowerUp.Name)
                    {
                        powerup = null;
                    }
                }
            } while (powerup == null);

            return powerup;
        }
    }
}

