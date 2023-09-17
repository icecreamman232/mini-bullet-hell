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
