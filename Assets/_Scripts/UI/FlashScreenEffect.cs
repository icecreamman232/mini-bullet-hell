using System.Collections;
using DG.Tweening;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class FlashScreenEffect : MonoBehaviour
    {
        [SerializeField] private float m_fadeInTime;
        [SerializeField] private float m_fadeOutTime;
        [SerializeField] private FloatEvent m_flashScreenEvent;
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        [SerializeField] private Image m_flashImage;
        [SerializeField] private FlashProtectorPowerUp m_flashProtectorPowerUp;
        
        private bool m_hasProcess;
        private bool m_forbidden;
        private void OnGameStateChange(GameState prev, GameState next)
        {
            if (next == GameState.END_WAVE
                || next == GameState.PICK_SKILL
                || next == GameState.GAME_OVER)
            {
                m_forbidden = true;
                m_hasProcess = false;
                StopAllCoroutines();
            }
            else if (next == GameState.FIGHTING)
            {
                m_forbidden = false;
            }
        }
        
        private void Start()
        {
            m_flashImage.SetAlpha(0);
            m_flashScreenEvent.AddListener(OnTriggerFlashScreen);
            m_gameCoreEvent.OnChangeStateCallback += OnGameStateChange;
        }

        private void OnTriggerFlashScreen(float duration)
        {
            if (m_forbidden)
            {
                return;
            }
            StartCoroutine(OnFlashingScreen(duration));
        }

        private IEnumerator OnFlashingScreen(float duration)
        {
            if (m_hasProcess)
            {
                yield break;
            }

            m_hasProcess = true;
            
            var flashPercent = m_flashProtectorPowerUp.IsActive ? MathHelpers.PercentOf(m_flashProtectorPowerUp.FlashReducePercent) : 1f;
            
            m_flashImage.DOFade(flashPercent, m_fadeInTime);
            yield return new WaitForSeconds(m_fadeInTime + duration);
            m_flashImage.DOFade(0, m_fadeOutTime);
            
            m_hasProcess = false;
        }
        
        private void OnDestroy()
        {
            m_gameCoreEvent.OnChangeStateCallback -= OnGameStateChange;
            m_flashScreenEvent.RemoveListener(OnTriggerFlashScreen);
        }
    }
}
