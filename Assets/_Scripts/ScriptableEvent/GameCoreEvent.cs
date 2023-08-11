using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    public enum GameState
    {
        FIGHTING,
        PICK_UPGRADE,
    }
    
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Game core event")]
    public class GameCoreEvent : ScriptableObject
    {
        [SerializeField] private GameState m_curState;

        public GameState CurrentState => m_curState;

        public Action<GameState,GameState> OnChangeStateCallback;
        
        public void SetGameState(GameState newState)
        {
            OnChangeStateCallback?.Invoke(m_curState, newState);
            m_curState = newState;
        }
    }
}

