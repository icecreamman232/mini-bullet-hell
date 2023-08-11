using System.Collections;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class UpgradeCanvas : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private IntEvent m_selectCardEvent;
        [Header("UI References")]
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private NextWaveButton m_nextWaveButton;
        private bool m_hasProcess;
        
        private void Start()
        {
            Hide();
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_nextWaveButton.OnClickCallback += ProcessNextWave;
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

        private void ProcessNextWave()
        {
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
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
        }

        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
        }
    }
}

