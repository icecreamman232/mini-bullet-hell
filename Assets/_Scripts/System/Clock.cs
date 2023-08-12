using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Common
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private float m_duration;
        [SerializeField] private FloatEvent m_durationEvent;

        public Action OnStartClock;
        public Action OnEndClock;
        
        public void SetTime(float duration)
        {
            m_duration = duration;
            OnStartClock?.Invoke();
            m_durationEvent.Raise(m_duration);
        }

        private void Update()
        {
            if (m_duration == 0) return;

            m_duration -= Time.deltaTime;

            if (m_duration <= 0)
            {
                m_duration = 0;
                OnEndClock?.Invoke();
            }
            
            m_durationEvent.Raise(m_duration);
        }
    }
}
