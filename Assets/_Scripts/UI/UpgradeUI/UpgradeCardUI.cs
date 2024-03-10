using System;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    [Serializable]
    public struct RankColor
    {
        public UpgradeRank Rank;
        public Color Foreground;
        public Color Background;
    }
    public class UpgradeCardUI : Selectable
    {
        [SerializeField] [ReadOnly] private AttributeUpgradeBase m_upgradeData;

        [Space] 
        [SerializeField] private Image m_outline;
        [SerializeField] private Image m_icon;
        [SerializeField] private Image m_background;
        [SerializeField] private Image m_foreground;
        [SerializeField] private TextMeshProUGUI m_nameTxt;
        [SerializeField] private TextMeshProUGUI m_descTxt;

        [Space] [Header("Rank Color Code")] 
        [SerializeField] private RankColor[] m_rankColors;
        
        public Action<UpgradeCardUI> OnSelectAction;

        public AttributeUpgradeBase UpgradeData => m_upgradeData;
        
        private void OnEnable()
        {
            m_outline.enabled = false;
        }
        
        public void AssignData(AttributeUpgradeBase data)
        {
            m_upgradeData = data;
            m_nameTxt.text = m_upgradeData.UpgradeName;

            switch (data.Rank)
            {
                case UpgradeRank.BRONZE:
                    m_background.color = m_rankColors[0].Background;
                    m_foreground.color = m_rankColors[0].Foreground;
                    break;
                case UpgradeRank.SILVER:
                    m_background.color = m_rankColors[1].Background;
                    m_foreground.color = m_rankColors[1].Foreground;
                    break;
                case UpgradeRank.GOLD:
                    m_background.color = m_rankColors[2].Background;
                    m_foreground.color = m_rankColors[2].Foreground;
                    break;
            }
            
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            m_outline.enabled = true;
            transform.localScale = Vector3.one * 1.15f;
        }
        
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            m_outline.enabled = false;
            transform.localScale = Vector3.one;
        }
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnSelectAction?.Invoke(this);
        }
    }
}

