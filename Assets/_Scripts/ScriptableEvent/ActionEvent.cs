using System;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Action Event")]
    public class ActionEvent : ScriptableObject
    {
        protected Action m_listeners;
        
        public void AddListener(Action addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise()
        {
            m_listeners?.Invoke();
        }
    } 
}

