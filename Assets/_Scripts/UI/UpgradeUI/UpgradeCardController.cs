using System;
using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class UpgradeCardController : Selectable
    {
        [SerializeField] private int m_cardIndex;
        [SerializeField] private IntEvent m_selectCardEvent;
        [SerializeField] private RuntimeWorldSet m_runtimeWorldSet;
        [SerializeField] private Image m_outLine;
        [SerializeField] private Image m_background;
        [SerializeField] private Image m_icon;
        [SerializeField] private TextMeshProUGUI m_title;
        [SerializeField] private TextMeshProUGUI m_desc;
        
        public void SetUpgrade(ComponentProfile profile)
        {
            switch (profile.Grade)
            {
                case Grade.Common:
                    m_background.color = m_runtimeWorldSet.CommonColor;
                    break;
                case Grade.Uncommon:
                    m_background.color = m_runtimeWorldSet.UncommonColor;
                    break;
                case Grade.Rare:
                    m_background.color = m_runtimeWorldSet.RareColor;
                    break;
                case Grade.Legend:
                    m_background.color = m_runtimeWorldSet.LegendColor;
                    break;
            }

            m_icon.sprite = profile.Icon;
            m_title.text = profile.Title;
            m_desc.text = $"+{profile.BonusValue.ToString()} + {profile.Description}";
        }
        
        
        
        public override void OnSelect(BaseEventData eventData)
        {
            m_outLine.gameObject.SetActive(true);
            m_selectCardEvent.Raise(m_cardIndex);
            base.OnSelect(eventData);
        }
        
        public override void OnDeselect(BaseEventData eventData)
        {
            m_outLine.gameObject.SetActive(false);
            base.OnSelect(eventData);
        }
    }
}

