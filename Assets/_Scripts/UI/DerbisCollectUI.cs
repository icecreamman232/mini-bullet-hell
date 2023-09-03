using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class DerbisCollectUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private ResourceEvent m_resourceEvent;
        [SerializeField] private TextMeshProUGUI m_collectAmount;

        private void Start()
        {
            m_collectAmount.text = "0";
            m_resourceEvent.OnChangeDerbisAmount += UpdateAmount;
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
        }

        private void OnShow()
        {
            m_canvasGroup.interactable = false;
            m_canvasGroup.DOFade(1, 1f).SetUpdate(true);
        }

        private void OnHide()
        {
            m_canvasGroup.interactable = false;
            m_canvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        }
        
        
        private void OnChangeGameState(GameState prev, GameState current)
        {
            if (current == GameState.GAME_OVER)
            {
                OnHide();
            }
            else if (current == GameState.FIGHTING)
            {
                OnShow();
            }
        }
        
        private void UpdateAmount(int amount)
        {
            m_collectAmount.text = amount.ToString();
        }
    }
}

