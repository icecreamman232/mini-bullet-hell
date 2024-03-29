using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Spawn profile")]
    public class SpawnProfile : ScriptableObject
    {
        public float DelayTimeBetweenTwoSpawn;

        public int AmountEnemyBeforeSpawnSpecial;
        
        [Header("Normal")]
        [SerializeField] private WeightObject[] m_weights;
        [SerializeField] private GameObject[] m_spawnArray;

        [FormerlySerializedAs("m_specialWeight")]
        [Header("Special enemy")] 
        [SerializeField] private WeightObject[] m_eliteWeight;
        [FormerlySerializedAs("m_specialEnemies")] [SerializeField] private GameObject[] m_eliteEnemies;

        private int m_spawnedEnemyBeforeSpecial;
        
        private void OnEnable()
        {
            m_spawnedEnemyBeforeSpecial = 0;
            WeightObject.ComputeSpawnArray(m_weights);
        }

        public List<GameObject> GetListSpawn()
        {
            m_spawnedEnemyBeforeSpecial++;
            var amount = Random.Range(1, 3);
            var list = new List<GameObject>();
            if (m_spawnedEnemyBeforeSpecial >= AmountEnemyBeforeSpawnSpecial)
            {
                list.Add(GetEliteSpawn());
                m_spawnedEnemyBeforeSpecial = 0;
            }
            for (int i = 0; i < amount; i++)
            {
                list.Add(GetNormalSpawn());
            }
            return list;    
        }
        
        public GameObject GetNextSpawn()
        {
            m_spawnedEnemyBeforeSpecial++;
            if (m_spawnedEnemyBeforeSpecial >= AmountEnemyBeforeSpawnSpecial)
            {
                m_spawnedEnemyBeforeSpecial = 0;
                return GetEliteSpawn();
            }
            else
            {
                return GetNormalSpawn();
            }
        }

        private GameObject GetEliteSpawn()
        {
            var random = Random.Range(0f, 100f);
            var index = WeightObject.GetWeightIndex(m_eliteWeight, random);
            return m_eliteEnemies[index];
        }
        private GameObject GetNormalSpawn()
        {
            var random = Random.Range(0f, 100f);
            var index = WeightObject.GetWeightIndex(m_weights, random);
            return m_spawnArray[index];
        }
    } 
}

