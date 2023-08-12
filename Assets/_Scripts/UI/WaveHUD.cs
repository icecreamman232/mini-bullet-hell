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

        [SerializeField] private FloatEvent m_waveTimeEvent;

        private int m_minute;
        private int m_seconds;
        
        private void Start()
        {
            m_waveTimeEvent.AddListener(UpdateTime);
        }

        private void UpdateTime(float time)
        {
            m_minute = Mathf.FloorToInt(time / 60f);
            m_seconds = (int)(time % 60);
            m_waveTimeTxt.text = $"{m_minute:D2}:{m_seconds:D2}";
        }
    }
}

