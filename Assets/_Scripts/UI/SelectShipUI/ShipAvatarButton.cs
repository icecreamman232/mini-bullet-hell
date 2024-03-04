using System;
using JustGame.Scripts.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class ShipAvatarButton : Selectable
    {
        [SerializeField] private Image m_avatar;
        [FormerlySerializedAs("m_shipProfile")] [SerializeField] private ShipAttribute shipAttribute;
        [SerializeField] private ShipSelectInfoPanel m_infoPanel;
        private Action<ShipAvatarButton> OnSelectAction;
        private Action<ShipAvatarButton> OnDeselectAction;
        public ShipAttribute ShipAttribute => shipAttribute;
        
        public Image ShipAvatar => m_avatar;
        protected override void Start()
        {
            base.Start();
            OnSelectAction += m_infoPanel.OnSelectShipAvatar; 
            OnDeselectAction += m_infoPanel.OnDeselectShipAvatar;
        }
        
        public override void OnSelect(BaseEventData eventData)
        {
            OnSelectAction?.Invoke(this);
            base.OnSelect(eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            OnDeselectAction?.Invoke(this);
            base.OnDeselect(eventData);
        }

        protected override void OnDestroy()
        {
            OnSelectAction -= m_infoPanel.OnSelectShipAvatar; 
            OnDeselectAction -= m_infoPanel.OnDeselectShipAvatar;
            base.OnDestroy();
        }
    }
}

