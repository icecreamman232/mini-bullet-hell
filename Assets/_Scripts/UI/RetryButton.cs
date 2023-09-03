using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class RetryButton : Selectable
    {
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        public override void OnPointerUp(PointerEventData eventData)
        {
            m_gameCoreEvent.SetGameState(GameState.INTRO);
            base.OnPointerUp(eventData);
        }
    }
}
 
