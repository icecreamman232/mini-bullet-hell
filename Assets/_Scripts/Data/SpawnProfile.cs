using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Data
{
    [Serializable]
    public class SpawnWaveInfo
    {
        public GameObject EnemyPrefab;
        public float DelayTime;
        public int MinQuantity;
        public int MaxQuantity;
    }
    
    [CreateAssetMenu(menuName = "JustGame/Data/Spawn profile")]
    public class SpawnProfile : ScriptableObject
    {
        [SerializeField] private SpawnWaveInfo[] m_waveInfo;

        public float GetDelayTime(int waveIndex)
        {
            return m_waveInfo[waveIndex].DelayTime;
        }

        public int GetQuantity(int waveIndex)
        {
            return Random.Range(m_waveInfo[waveIndex].MinQuantity, m_waveInfo[waveIndex].MaxQuantity + 1);
        }
        
        public GameObject GetNextEnemyPrefab(int waveIndex)
        {
            return m_waveInfo[waveIndex].EnemyPrefab;
        }
    } 
}

