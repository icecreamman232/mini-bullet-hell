using System;
using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class DeathScreenUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private RetryButton m_retryButton;
        private void Start()
        {
            OnHide();
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
        }

        private void OnChangeGameState(GameState prevState, GameState curState)
        {
            if (curState == GameState.GAME_OVER)
            {
                OnShow();
            }
            else if (curState == GameState.FIGHTING)
            {
                OnHide();
            }
        }
        
        private void OnShow()
        {
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_canvasGroup.DOFade(1, 0.3f).SetUpdate(true);
        }

        private void OnHide()
        {
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            m_canvasGroup.alpha = 0;
        }

        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
        }
    }
}

