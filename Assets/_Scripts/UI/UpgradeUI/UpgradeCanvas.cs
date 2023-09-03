using System;
using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class UpgradeCanvas : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private IntEvent m_selectCardEvent;
        [SerializeField] private PlayerData m_playerData;
        [Header("UI References")]
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private NextWaveButton m_nextWaveButton;
        [SerializeField] private UpgradeCardController m_card1;
        [SerializeField] private UpgradeCardController m_card2;
        [SerializeField] private UpgradeCardController m_card3;

        private ComponentProfile[] m_profiles;
        // private ComponentProfile m_profile1;
        // private ComponentProfile m_profile2;
        // private ComponentProfile m_profile3;
        private bool m_hasProcess;
        private int m_selectedCard;
        
        private void Start()
        {
            Hide();
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_nextWaveButton.OnClickCallback += ProcessNextWave;
            m_selectCardEvent.AddListener(ChangeCard);
            m_profiles = new ComponentProfile[3];
        }

        private void OnChangeGameState(GameState prevState, GameState newState)
        {
            switch (newState)
            {
                case GameState.FIGHTING:
                    Hide();
                    break;
                case GameState.PICK_UPGRADE:
                    Show();
                    StartCoroutine(ShowNextButtonRoutine());
                    break;
            }
        }

        private void ChangeCard(int index)
        {
            m_selectedCard = index;
        }
        private void ProcessNextWave()
        {
            // var profile = m_profiles[m_selectedCard];
            // switch (profile.ComponentType)
            // {
            //     case ComponentType.REACTOR:
            //         m_playerData.IncreaseReactorPoint(profile.BonusValue);
            //         break;
            //     case ComponentType.ENGINE:
            //         m_playerData.IncreaseEnginePoint(profile.BonusValue);
            //         break;
            //     case ComponentType.HULL:
            //         m_playerData.IncreaseHullPoint(profile.BonusValue);
            //         break;
            // }
            //
            m_gameCoreEvent.SetGameState(GameState.FIGHTING);
        }

        private IEnumerator ShowNextButtonRoutine()
        {
            if (m_hasProcess)
            {
                yield break;
            }

            m_hasProcess = true;
            yield return new WaitForSecondsRealtime(0.5f);
            m_nextWaveButton.gameObject.SetActive(true);
            m_hasProcess = false;
        }
        
        private void Show()
        {
            SetupCards();
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
        }

        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
        }

        private void SetupCards()
        {
            m_profiles[0] = UpgradeManager.Instance.GetRandomProfile();
            m_profiles[1] = UpgradeManager.Instance.GetRandomProfile();
            m_profiles[2] = UpgradeManager.Instance.GetRandomProfile();
            
            m_card1.SetUpgrade(m_profiles[0]);
            m_card2.SetUpgrade(m_profiles[1]);
            m_card3.SetUpgrade(m_profiles[2]);
        }

        private void OnDestroy()
        {
            m_selectCardEvent.RemoveListener(ChangeCard);
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
        }
    }
}

