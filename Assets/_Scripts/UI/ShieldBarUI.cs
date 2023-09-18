using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class ShieldBarUI : MonoBehaviour
    {
        [SerializeField] private ShieldPowerUp m_shieldPowerUp;
        [SerializeField] private IntEvent m_shieldBarRemoveEvent;
        [SerializeField] private BoolEvent m_cooldownDoneEvent;
        [SerializeField] private GameObject[] m_shieldIcons;

        private void Awake()
        {
            m_shieldBarRemoveEvent.AddListener(HideShieldIconOnHit);
            m_cooldownDoneEvent.AddListener(ResetShieldBar);
            m_shieldPowerUp.OnApplyPowerUp += TriggerShieldUI;
            m_shieldPowerUp.OnDiscardPowerUp += DiscardShieldUI;
        }

        private void TriggerShieldUI()
        {
            ResetShieldBar(true);
        }

        private void DiscardShieldUI()
        {
            for (int i = 0; i < m_shieldIcons.Length; i++)
            {
                m_shieldIcons[i].SetActive(false);
            }
        }

        private void ResetShieldBar(bool value)
        {
            if (value)
            {
                StartCoroutine(ResetShieldRoutine());
            }
        }

        private IEnumerator ResetShieldRoutine()
        {
            for (int i = 0; i < m_shieldIcons.Length; i++)
            {
                m_shieldIcons[i].SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private void HideShieldIconOnHit(int index)
        {
            m_shieldIcons[index].SetActive(false);
        }

        private void OnDestroy()
        {
            m_shieldBarRemoveEvent.RemoveListener(HideShieldIconOnHit);
            m_cooldownDoneEvent.RemoveListener(ResetShieldBar);
            m_shieldPowerUp.OnApplyPowerUp -= TriggerShieldUI;
            m_shieldPowerUp.OnDiscardPowerUp -= DiscardShieldUI;
        }
    }
}

