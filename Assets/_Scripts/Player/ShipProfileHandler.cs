using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class ShipProfileHandler : MonoBehaviour
    {
        [SerializeField] private ShipProfile m_shipProfile;
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        private void Start()
        {
            m_playerData.SetDefaultValue(
                m_shipProfile.BaseReactorPoint,
                m_shipProfile.BaseEnginePoint, 
                m_shipProfile.BaseHullPoint);

            ApplyValue();
        }

        private void ApplyValue()
        {
            var movementSpeed = m_shipProfile.BaseEnginePoint * 5;
            var health = m_shipProfile.BaseHullPoint * 10;
            
            m_playerComponentSet.Movement.SetSpeed(movementSpeed);
            m_playerComponentSet.Health.SetMaxHealth(health);
        }
    }
}

