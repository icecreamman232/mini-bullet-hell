using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class NextWaveButton : Selectable
    {
        public Action OnClickCallback;

        public override void OnPointerUp(PointerEventData eventData)
        {
            OnClickCallback?.Invoke();
            base.OnPointerUp(eventData);
        }
    }  
}

