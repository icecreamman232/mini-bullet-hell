using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class PlayGameButton : Selectable
    {
        public Action OnClick;
        public override void OnPointerUp(PointerEventData eventData)
        {
            OnClick?.Invoke();
            base.OnPointerUp(eventData);
        }
    }
}
