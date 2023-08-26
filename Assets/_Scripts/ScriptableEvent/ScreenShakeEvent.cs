using System;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Shake Event")]
    public class ScreenShakeEvent : ScriptableObject
    {
        protected Action<ShakeProfile> m_listeners;

        public void AddListener(Action<ShakeProfile> addListener)
        {
            m_listeners += addListener;
        }
        
        public void RemoveListener(Action<ShakeProfile> removeListener)
        {
            m_listeners += removeListener;
        }

        public void DoShake(ShakeProfile profile)
        {
            m_listeners.Invoke(profile);
        }
    }
}

