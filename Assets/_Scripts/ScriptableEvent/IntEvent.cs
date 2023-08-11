using System;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Int Event")]
    public class IntEvent : ScriptableObject
    {
        protected Action<int> m_listeners;
        
        public void AddListener(Action<int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(int value)
        {
            m_listeners?.Invoke(value);
        }
    }
}

