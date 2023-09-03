using System;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class WaveHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_waveNameTxt;
        [SerializeField] private TextMeshProUGUI m_waveTimeTxt;
        [SerializeField] private IntEvent m_waveCountEvent;
        [SerializeField] private FloatEvent m_waveTimeEvent;

        private int m_minute;
        private int m_seconds;
        
        private void Start()
        {
            m_waveTimeEvent.AddListener(UpdateTime);
            m_waveCountEvent.AddListener(UpdateWaveTxt);
        }

        private void UpdateWaveTxt(int waveCount)
        {
            m_waveNameTxt.text = $"Wave {waveCount.ToString()}";
        }
        
        private void UpdateTime(float time)
        {
            m_minute = Mathf.FloorToInt(time / 60f);
            m_seconds = (int)(time % 60);
            m_waveTimeTxt.text = $"{m_minute:D2}:{m_seconds:D2}";
        }

        private void OnDestroy()
        {
            m_waveTimeEvent.RemoveListener(UpdateTime);
            m_waveCountEvent.RemoveListener(UpdateWaveTxt);
        }
    }
}

