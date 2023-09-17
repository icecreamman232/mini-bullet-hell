using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class UpgradeNodeUI : Selectable
    {
        [Header("UI")]
        [SerializeField] private Image m_iconBG;
        [SerializeField] private Image m_progressBar;
        [Header("Upgrade")] 
        [SerializeField] private ResourceEvent m_resourceEvent;
        [SerializeField] private UpgradeProfile m_upgradeProfile;
        [Header("Branches")] 
        [SerializeField] private GameObject[] m_branches;
        
        private readonly float m_delayBetweenTwoSpending = 0.2f;
        private float m_timer;
        private bool m_isPressingDown;
        
        public void Initialize()
        {
            m_progressBar.fillAmount = 0;
        }
        
        private void Show()
        {
            m_iconBG.raycastTarget = true;
        }

        private void Hide()
        {
            m_iconBG.raycastTarget = false;
        }

        private void OnUnlockOtherNodes()
        {
            for (int i = 0; i < m_branches.Length; i++)
            {
                m_branches[i].gameObject.SetActive(true);
            }
        }
        
        private void Update()
        {
            if (m_upgradeProfile == null) return;
            if (m_upgradeProfile.IsUnlocked) return;
            if(!m_isPressingDown) return;

            m_timer += Time.unscaledDeltaTime;
            if (m_timer >= m_delayBetweenTwoSpending)
            {
                m_timer = 0;
                if (!m_resourceEvent.CanSpend) return;
                m_resourceEvent.SpendDerbis(1);
                m_upgradeProfile.SpentXP(1);
                m_progressBar.fillAmount = ((float)(m_upgradeProfile.CurrentXP)) / m_upgradeProfile.XPToUnlock;
                if (m_upgradeProfile.IsUnlocked)
                {
                    OnUnlockOtherNodes();
                }
            }
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (m_upgradeProfile.IsUnlocked) return;
            if (m_isPressingDown) return;
            m_isPressingDown = true;
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (m_upgradeProfile.IsUnlocked) return;
            if (!m_isPressingDown) return;
            m_isPressingDown = false;
            base.OnPointerUp(eventData);
        }
    }
}
