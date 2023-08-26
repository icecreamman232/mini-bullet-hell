using System;
using JustGame.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class SkillCardUI : Selectable
    {
        [SerializeField] private Image m_outline;
        [SerializeField] private Image m_icon;
        [SerializeField] private TextMeshProUGUI m_powerUpName;
        [SerializeField] private PowerUpData m_powerUp;

        public Action<SkillCardUI> OnSelectAction;
        public PowerUpData PowerUp => m_powerUp;
        
        private void OnEnable()
        {
            m_outline.enabled = false;
        }

        public void AssignPowerUp(PowerUpData powerUpData)
        {
            m_powerUp = powerUpData;
            m_powerUpName.text = m_powerUp.Name;
            m_icon.sprite = powerUpData.Icon;
        }
        
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            m_outline.enabled = true;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            m_outline.enabled = false;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnSelectAction?.Invoke(this);
        }
    }
}

