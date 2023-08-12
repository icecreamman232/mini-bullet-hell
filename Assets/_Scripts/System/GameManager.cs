using JustGame.Scripts.Common;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Wave")] 
        [SerializeField] private WaveEvent m_waveEvent;
        [SerializeField] private IntEvent m_waveCountEvent;
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private Clock m_clock;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        [SerializeField] private bool m_isPaused;
        private InputManager m_inputManager;

        public bool IsPaused => m_isPaused;
        
        private void Start()
        {
            m_inputManager = InputManager.Instance;
            m_runtimeWorldSet.SetGameManager(this);
            
            m_waveCountEvent.Raise(m_waveEvent.CurrentWave);
            m_clock.SetTime( m_waveEvent.GetWaveDuration(m_waveEvent.CurrentWave));
            
            m_clock.OnEndClock += OnFinishWaveTime;
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_gameCoreEvent.SetGameState(GameState.FIGHTING);
        }

        private void PauseGame(bool value)
        {
            m_isPaused = value;
            Time.timeScale = m_isPaused ? 0 : 1;
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

        private void OnFinishWaveTime()
        {
            m_gameCoreEvent.SetGameState(GameState.PICK_UPGRADE);
        }
    }
}

