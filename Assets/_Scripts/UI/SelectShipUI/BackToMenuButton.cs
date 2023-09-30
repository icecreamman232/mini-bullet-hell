using JustGame.Scripts.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class BackToMenuButton : Selectable
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            SceneLoader.Instance.LoadToScene("ShipSelectionScene", "MenuScene");
            base.OnPointerUp(eventData);
        }
    }
}

