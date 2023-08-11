using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerStat : MonoBehaviour
    {
        [SerializeField] private ShipProfile m_shipProfile;
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        private void Start()
        {
            m_playerData.SetDefaultValue(
                m_shipProfile.BaseReactorPoint,
                m_shipProfile.BaseEnginePoint, 
                m_shipProfile.BaseHullPoint);

            ApplyValue();

            m_gameCoreEvent.OnChangeStateCallback += OnGameStateChange;
        }

        private void OnGameStateChange(GameState prevState, GameState nextState)
        {
            if (nextState == GameState.FIGHTING)
            {
                ApplyValue();
            }
        }
        
        private void ApplyValue()
        {
            var movementSpeed = m_playerData.EnginePts * 5;
            var health = m_playerData.HullPts * 10;
            
            m_playerComponentSet.Movement.SetSpeed(movementSpeed);
            m_playerComponentSet.Health.SetMaxHealth(health);
        }
    }
}

