using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Wave event")]
    public class WaveEvent : ScriptableObject
    {
        public int MaxWave;
        public int CurrentWave;

        [SerializeField] private List<float> m_waveDurations; 
        
        private void OnEnable()
        {
            CurrentWave = 0;
        }

        public float GetWaveDuration(int index)
        {
            return m_waveDurations[index];
        }
        
        public void IncreaseWave()
        {
            CurrentWave++;
        }
    }
}

