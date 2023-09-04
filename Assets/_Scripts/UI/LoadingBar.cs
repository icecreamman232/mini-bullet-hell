using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private FloatEvent m_loadingEvent;
        [SerializeField] private Image m_loadingBar;

        private void Awake()
        {
            m_loadingEvent.AddListener(UpdateBar);
        }

        private void UpdateBar(float value)
        {
            m_loadingBar.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_loadingEvent.RemoveListener(UpdateBar);
        }
    }
}
