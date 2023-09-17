using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class UpgradeNodeUI : Selectable
    {
        [Header("UI")]
        [SerializeField] private Image m_icon;
        [SerializeField] private Image m_progressBar;
        [Header("Upgrade")] 
        [SerializeField] private ResourceEvent m_resourceEvent;
        [SerializeField] private UpgradeProfile m_upgradeProfile;

        private readonly float m_delayBetweenTwoSpending = 0.2f;
        private float m_timer;
        private bool m_isPressingDown;
        
        public void Initialize()
        {
            m_progressBar.fillAmount = 0;
        }
        
        private void Show()
        {
            m_icon.raycastTarget = true;
        }

        private void Hide()
        {
            m_icon.raycastTarget = false;
        }

        private void Update()
        {
            if(!m_isPressingDown) return;

            m_timer += Time.unscaledDeltaTime;
            if (m_timer >= m_delayBetweenTwoSpending)
            {
                m_timer = 0;
                if (m_upgradeProfile == null) return;
                if (!m_resourceEvent.CanSpend) return;
                m_resourceEvent.SpendDerbis(1);
                m_upgradeProfile.SpentXP(1);
                m_progressBar.fillAmount = ((float)(m_upgradeProfile.CurrentXP)) / m_upgradeProfile.XPToUnlock;
            }
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (m_isPressingDown) return;
            m_isPressingDown = true;
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!m_isPressingDown) return;
            
            m_isPressingDown = false;
            base.OnPointerUp(eventData);
        }
    }
}
