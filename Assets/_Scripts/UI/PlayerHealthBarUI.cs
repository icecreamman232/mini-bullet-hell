using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_reduceSpeed;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private FloatEvent m_healthEvent;
        [SerializeField] private Image m_damageImg;
        [SerializeField] private Image m_currentImg;

        private float m_target;

        private void Start()
        {
            m_canvasGroup.interactable = false;
            m_healthEvent.AddListener(UpdateHealthBar);
            m_gameCoreEvent.OnChangeStateCallback += OnChangeGameState;
            m_target = 1;
            m_damageImg.fillAmount = 1;
            m_currentImg.fillAmount = 1;
        }

        private void OnShow()
        {
            m_canvasGroup.DOFade(1, 1f).SetUpdate(true);
        }
        
        private void OnHide()
        {
            m_canvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        }
        
        private void Update()
        {
            if (m_damageImg.fillAmount <= m_target)
            {
                return;
            }

            m_damageImg.fillAmount -= Time.deltaTime * m_reduceSpeed;

            if (m_damageImg.fillAmount <= 0)
            {
                m_damageImg.fillAmount = 0;
            }
        }

        private void UpdateHealthBar(float percent)
        {
            m_target = percent;
            m_currentImg.fillAmount = percent;
        }

        private void OnChangeGameState(GameState prevState, GameState curState)
        {
            if (curState == GameState.GAME_OVER)
            {
                OnHide();
            }
            else if (curState == GameState.FIGHTING)
            {
                OnShow();
            }
        }
        
        private void OnDestroy()
        {
            m_healthEvent.RemoveListener(UpdateHealthBar);
            m_gameCoreEvent.OnChangeStateCallback -= OnChangeGameState;
        }
    }
}

