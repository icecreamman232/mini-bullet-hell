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
        [SerializeField] private Image m_flashImage;
        [SerializeField] private FlashProtectorPowerUp m_flashProtectorPowerUp;
        
        private bool m_hasProcess;
        
        private void Start()
        {
            m_flashImage.SetAlpha(0);
            m_flashScreenEvent.AddListener(OnTriggerFlashScreen);
        }

        private void OnTriggerFlashScreen(float duration)
        {
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
            m_flashScreenEvent.RemoveListener(OnTriggerFlashScreen);
        }
    }
}
