using JustGame.Scripts.Common;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class RetryButton : Selectable
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            SceneLoader.Instance.LoadToScene("GameplayScene","MenuScene");
            base.OnPointerUp(eventData);
        }
    }
}
 
