using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class XPUIController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_xpImage;
        [SerializeField] private TextMeshProUGUI m_levelNumber;
        [Space]
        [Header("Data")]
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private FloatEvent m_xpUpdateEvent;
        [SerializeField] private IntEvent m_lvlUpEvent;

        private void Start()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_xpImage.fillAmount = 0;
            m_levelNumber.text = "0";
            m_xpUpdateEvent.AddListener(OnGainXP);
            m_lvlUpEvent.AddListener(OnLevelUp);
            m_gameCoreEvent.OnChangeStateCallback += OnChangeStateCallback;
        }
        

        private void Show()
        {
            m_canvasGroup.alpha = 1;
        }

        private void Hide()
        {
            m_canvasGroup.alpha = 0;
        }

        private void OnGainXP(float percent)
        {
            m_xpImage.fillAmount = percent;
        }

        private void OnLevelUp(int level)
        {
            m_levelNumber.text = level.ToString();
            m_xpImage.fillAmount = 0;
        }
        
        private void OnChangeStateCallback(GameState prevState, GameState curState)
        {
            if (curState == GameState.GAME_OVER)
            {
                Hide();
            }
            else if (curState == GameState.FIGHTING)
            {
                Show();
            }
        }

        private void OnDestroy()
        {
            m_xpUpdateEvent.RemoveListener(OnGainXP);
            m_lvlUpEvent.RemoveListener(OnLevelUp);
        }
    }
}

