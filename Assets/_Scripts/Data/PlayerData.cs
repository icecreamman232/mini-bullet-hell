using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Player data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Core Stats")]
        [SerializeField] private int m_reactorPoint;
        [SerializeField] private int m_enginePoint;
        [SerializeField] private int m_hullPoint;
        
        [Header("Secondary Stats")]
        //Conversion rate from reactor point to energy. Ex: 2 mean rate 1:2 (reactor:energy)
        [SerializeField] private int m_energyConversionRate;
        [SerializeField] private int m_energy;

        [Header("Exp")] 
        [SerializeField] private float m_expRate;
        
        public int ReactorPts => m_reactorPoint;
        public int EnginePts => m_enginePoint;
        public int HullPts => m_hullPoint;

        public int EnergyConversionRate => m_energyConversionRate;
        public int Energy => m_energy;

        public float ExpRate => m_expRate;
        
        private void OnEnable()
        {
            m_reactorPoint = 0;
            m_enginePoint = 0;
            m_hullPoint = 0;

            m_energyConversionRate = 0;
            m_energy = 0;

            m_expRate = 100;
        }
        
        public void IncreaseReactorPoint(int addValue)
        {
            m_reactorPoint += addValue;
        }
        
        public void IncreaseEnginePoint(int addValue)
        {
            m_enginePoint += addValue;
        }
        
        public void IncreaseHullPoint(int addValue)
        {
            m_hullPoint += addValue;
        }

        public void IncreaseXpRate(float AddPercent)
        {
            m_expRate += AddPercent;
        }
    }
}
