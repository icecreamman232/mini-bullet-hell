using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class UpgradeCardController : Selectable
    {
        [SerializeField] private int m_cardIndex;
        [SerializeField] private IntEvent m_selectCardEvent;
        [SerializeField] private Image m_outLine;


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

