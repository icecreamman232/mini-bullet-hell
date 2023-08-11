using System;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Bool Event")]
    public class BoolEvent : ScriptableObject
    {
        protected Action<bool> m_listeners;
        
        public void AddListener(Action<bool> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<bool> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(bool value)
        {
            m_listeners?.Invoke(value);
        }
    }
}
