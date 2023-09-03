using System;
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
            var movementSpeed =  m_shipProfile.BaseEnginePoint * 5 +  m_playerData.EnginePts * 0.5f;
            var health = m_shipProfile.BaseHullPoint * 10 + m_playerData.HullPts * 5;
            
            m_playerComponentSet.Movement.SetSpeed(movementSpeed);
            m_playerComponentSet.Health.SetMaxHealth(health);
        }

        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnGameStateChange;
        }
    }
}

