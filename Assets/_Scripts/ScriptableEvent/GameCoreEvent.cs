using System;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    public enum GameState
    {
        NONE = -1,
        FIGHTING,
        END_WAVE,
        PICK_SKILL,
        PICK_UPGRADE,
        GAME_OVER,
        INTRO,
        READY_TO_UPGRADE,
    }
    
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Game core event")]
    public class GameCoreEvent : ScriptableObject
    {
        [SerializeField] private GameState m_curState;

        public GameState CurrentState => m_curState;

        public Action<GameState,GameState> OnChangeStateCallback;


        private void OnEnable()
        {
            m_curState = GameState.NONE;
        }

        public void SetGameState(GameState newState)
        {
            OnChangeStateCallback?.Invoke(m_curState, newState);
            m_curState = newState;
        }
    }
}

