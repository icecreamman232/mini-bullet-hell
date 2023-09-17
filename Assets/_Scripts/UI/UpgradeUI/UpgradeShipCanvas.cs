using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class UpgradeShipCanvas : MonoBehaviour
    {
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private CanvasGroup m_canvasGroup;
        private bool m_isOpening;
        private void Awake()
        {
            m_gameCoreEvent.OnChangeStateCallback += OnGameStateChange;
            Hide();
        }
        
        private void OnGameStateChange(GameState prev, GameState current)
        {
            if (current == GameState.PICK_UPGRADE)
            {
                Show();
                m_isOpening = true;
            }
            else if (current == GameState.FIGHTING && m_isOpening)
            {
                Hide();
                m_isOpening = false;
            }
        }

        private void Show()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
        }
        
        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
        }
        
        
        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnGameStateChange;
        }
    }
}

