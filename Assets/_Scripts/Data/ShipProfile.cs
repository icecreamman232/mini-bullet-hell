using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Ship profile")]
    public class ShipProfile : ScriptableObject
    {
        [SerializeField] private string m_shipName;
        [SerializeField] private int m_baseReactorPoint;
        [SerializeField] private int m_baseEnginePoint;
        [SerializeField] private int m_baseHullPoint;

        public int BaseReactorPoint => m_baseReactorPoint;
        public int BaseEnginePoint => m_baseEnginePoint;
        public int BaseHullPoint => m_baseHullPoint;
        
    }  
}

