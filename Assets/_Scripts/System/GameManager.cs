using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        [SerializeField] private bool m_isPaused;
        private InputManager m_inputManager;
        
        private void Start()
        {
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_inputManager = InputManager.Instance;
        }

        private void OnChangeGameState(GameState prevState, GameState newState)
        {
            switch (newState)
            {
                case GameState.FIGHTING:
                    m_pauseGameEvent.Raise(false);
                    PauseGame(false);
                    break;
                case GameState.PICK_UPGRADE:
                    m_inputManager.Reset();
                    m_pauseGameEvent.Raise(true);
                    PauseGame(true);
                    break;
            }
        }
        
        
        
        private void PauseGame(bool value)
        {
            m_isPaused = value;
            Time.timeScale = m_isPaused ? 0 : 1;
        }
    }
}

