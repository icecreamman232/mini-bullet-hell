using System.Collections;
using JustGame.Scripts.Attribute;
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
        [SerializeField] private PlayerSettings m_settings;
        //[SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private Transform m_spawnPoint;
        [Header("References")]
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        [SerializeField] private ActionEvent m_onPickedUpgradeEvent;
        [SerializeField] private bool m_isPaused;
        [Header("Wave")] 
        [SerializeField] private float m_waveDuration;
        [SerializeField] private bool m_endless;
        [SerializeField] private WaveEvent m_waveEvent;
        [SerializeField] private IntEvent m_waveCountEvent;
        [SerializeField] [ReadOnly] private int m_lastLevel;
        
        private InputManager m_inputManager;
        private int m_upgradeNumber;
        
        public bool IsPaused => m_isPaused;

        private void Awake()
        {
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_onPickedUpgradeEvent.AddListener(OnPickedUpgrade);
        }

        private IEnumerator Start()
        {
            m_inputManager = InputManager.Instance;
            m_runtimeWorldSet.SetGameManager(this);
            TimeManager.Instance.OnEndTime += OnFinishWaveTime;
            yield return new WaitUntil(() => !SceneLoader.Instance.IsProcessing);
            CaseIntro();
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
                case GameState.END_WAVE:
                    CaseEndWave();
                    break;
                case GameState.READY_TO_UPGRADE:
                    CaseReadyToPickUpgrade();
                    break;
                case GameState.PICK_UPGRADE:
                    
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
            Instantiate(m_settings.shipAttribute.ShipPrefab, m_spawnPoint.position, Quaternion.identity);
            m_gameCoreEvent.SetGameState(GameState.FIGHTING);
        }

        private void CaseFighting()
        {
            //Activate special ability
            if (m_settings.shipAttribute.SpecialAbility != null
                && !m_settings.shipAttribute.SpecialAbility.IsActive)
            {
                m_settings.shipAttribute.SpecialAbility.ApplyPowerUp();
            }
            
            m_inputManager.IsInputActive = true;
            m_pauseGameEvent.Raise(false);
            PauseGame(false);
            m_waveEvent.IncreaseWave();
            m_waveCountEvent.Raise(m_waveEvent.CurrentWave);
            TimeManager.Instance.SetTime(m_waveDuration);
            m_lastLevel = m_playerComponentSet.PlayerLevel;
        }

        private void CaseEndWave()
        {
            m_inputManager.IsInputActive = false;

            //Number of upgrade player can have after the wave ends.
            //Each level will give player 1 upgrade
            m_upgradeNumber = m_playerComponentSet.PlayerLevel - m_lastLevel;
            
            
            m_gameCoreEvent.SetGameState(GameState.READY_TO_UPGRADE);
        }
        private void SetToPickUpgrade()
        {
            m_gameCoreEvent.SetGameState(GameState.PICK_UPGRADE);
        }
        
        private void SetToPickSkill()
        {
            m_gameCoreEvent.SetGameState(GameState.PICK_SKILL);
        }
        
        private void CasePickSkill()
        {
            m_pauseGameEvent.Raise(true);
            PauseGame(true);
        }

        private void OnPickedUpgrade()
        {
            m_upgradeNumber--;
        }
        
        private void CaseReadyToPickUpgrade()
        {
            if (m_upgradeNumber > 0)
            {
                Invoke(nameof(SetToPickUpgrade),0.5f);    
            }
            else
            {
                Invoke(nameof(SetToPickSkill),0.5f);    
            }
        }
        
        private void CaseGameOver()
        {
            m_inputManager.IsInputActive = false;
            TimeManager.Instance.StopTime();
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
                m_gameCoreEvent.SetGameState(GameState.END_WAVE);
            }
        }

        private void OnDestroy()
        {
            if (TimeManager.Instance != null)
            {
                TimeManager.Instance.OnEndTime -= OnFinishWaveTime;
            }
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
            m_onPickedUpgradeEvent.RemoveListener(OnPickedUpgrade);
        }
    }
}

