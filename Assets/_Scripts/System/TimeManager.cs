using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] private float m_duration;
        [SerializeField] private FloatEvent m_durationEvent;

        public Action OnStartTime;
        public Action OnEndTime;

        public void SetTime(float duration)
        {
            m_duration = duration;
            OnStartTime?.Invoke();
            m_durationEvent.Raise(m_duration);
        }

        public void StopTime()
        {
            m_duration = 0;
            OnEndTime.Invoke();
        }

        private void Update()
        {
            if (m_duration == 0) return;
            m_duration -= Time.deltaTime;
            if (m_duration <= 0)
            {
                StopTime();
            }
            m_durationEvent.Raise(m_duration);
        }
    } 
}

