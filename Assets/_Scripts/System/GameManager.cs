using System;
using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Player")] 
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private Transform m_spawnPoint;
        [Header("References")]
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private LevelDataSO m_levelData;
        [SerializeField] private Clock m_clock;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        [SerializeField] private bool m_isPaused;
        [Header("Wave")] 
        [SerializeField] private bool m_endless;
        [SerializeField] private WaveEvent m_waveEvent;
        [SerializeField] private IntEvent m_waveCountEvent;
        
        private InputManager m_inputManager;
        public bool IsPaused => m_isPaused;
        
        private IEnumerator Start()
        {

            m_inputManager = InputManager.Instance;
            m_runtimeWorldSet.SetGameManager(this);
            m_clock.OnEndClock += OnFinishWaveTime;
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            yield return new WaitUntil(() => !SceneLoader.Instance.IsProcessing);
            m_gameCoreEvent.SetGameState(GameState.INTRO);

        }

        private void OnChangeGameState(GameState prevState, GameState newState)
        {
            m_inputManager.Reset();
            
            switch (newState)
            {
                case GameState.INTRO:
                    CaseIntro();
                    break;
                case GameState.FIGHTING:
                    CaseFighting();
                    break;
                case GameState.PICK_UPGRADE:
                    CasePickUpgrade();
                    break;
                case GameState.PICK_SKILL:
                    CasePickSkill();
                    break;
                case GameState.GAME_OVER:
                    CaseGameOver();
                    break;
            }
        }
        #region Game State
        private void CaseIntro()
        {
            Instantiate(m_playerPrefab, m_spawnPoint.position, Quaternion.identity);
            m_gameCoreEvent.SetGameState(GameState.FIGHTING);
        }

        private void CaseFighting()
        {
            m_inputManager.IsInputActive = true;
            m_pauseGameEvent.Raise(false);
            PauseGame(false);
            m_waveEvent.IncreaseWave();
            m_waveCountEvent.Raise(m_waveEvent.CurrentWave);
            m_clock.SetTime( m_waveEvent.GetWaveDuration(m_waveEvent.CurrentWave));
        }

        private void CasePickUpgrade()
        {
            m_pauseGameEvent.Raise(true);
            PauseGame(true);
        }
        
        private void CasePickSkill()
        {
            m_pauseGameEvent.Raise(true);
            PauseGame(true);
        }
        
        private void CaseGameOver()
        {
            m_inputManager.IsInputActive = false;
            m_clock.Stop();
            m_playerComponentSet.Reset();
            // m_pauseGameEvent.Raise(true);
            // PauseGame(true);
        }
        
        #endregion
        
        public void PauseGame(bool value)
        {
            m_isPaused = value;
            Time.timeScale = m_isPaused ? 0 : 1;
        }
        
        private void OnFinishWaveTime()
        {
            if (!m_endless)
            {
                m_gameCoreEvent.SetGameState(GameState.PICK_SKILL);
            }
            m_levelData.LevelUp();
        }

        private void OnDestroy()
        {
            m_clock.OnEndClock -= OnFinishWaveTime;
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
        }
    }
}

