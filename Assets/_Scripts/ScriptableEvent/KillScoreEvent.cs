using System;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Kill score event")]
    public class KillScoreEvent : ScriptableObject
    {
        [SerializeField] private int m_killScore;
        [SerializeField] private LevelDataSO m_levelData;
        
        public int KillScore => m_killScore;
        private Action<int> m_OnUpdateKillScore;

        private void OnEnable()
        {
            m_killScore = 0;
        }

        public void AddListener(Action<int> addListener)
        {
            m_OnUpdateKillScore += addListener;
        }

        public void RemoveListener(Action<int> removeListener)
        {
            m_OnUpdateKillScore -= removeListener;
        }
        
        
        public void AddKillScore(int value)
        {
            m_killScore += value;
            m_OnUpdateKillScore?.Invoke(m_killScore);
        }
    }
}

