using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class ChoosePowerUpButton : Selectable
    {
        public Action OnClickUp;
        public override void OnPointerUp(PointerEventData eventData)
        {
            OnClickUp?.Invoke();
            base.OnPointerUp(eventData);
        }
    }
}
