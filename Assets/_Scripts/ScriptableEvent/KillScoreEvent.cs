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
        [SerializeField] private GameCoreEvent m_gameCoreEvent;
        
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
            
            if (m_killScore >= m_levelData.CurrentLvlData.KillRequires)
            {
                m_killScore = 0;
                m_OnUpdateKillScore?.Invoke(m_killScore);
                //m_levelData.LevelUp();
                
                //Pause game to pick upgrades
                //m_gameCoreEvent.SetGameState(GameState.PICK_UPGRADE);
            }
            else
            {
                m_OnUpdateKillScore?.Invoke(m_killScore);
            }
        }
    }
}

