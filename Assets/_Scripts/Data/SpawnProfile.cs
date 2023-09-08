using UnityEngine;
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

        [Header("Special enemy")] 
        [SerializeField] private WeightObject[] m_specialWeight;
        [SerializeField] private GameObject[] m_specialEnemies;

        private int m_spawnedEnemyBeforeSpecial;
        
        private void OnEnable()
        {
            WeightObject.ComputeSpawnArray(m_weights);
        }

        public GameObject GetNextSpawn()
        {
            m_spawnedEnemyBeforeSpecial++;
            if (m_spawnedEnemyBeforeSpecial >= AmountEnemyBeforeSpawnSpecial)
            {
                m_spawnedEnemyBeforeSpecial = 0;
                return GetSpecialSpawn();
            }
            else
            {
                return GetNormalSpawn();
            }
        }

        private GameObject GetSpecialSpawn()
        {
            var random = Random.Range(0f, 100f);
            var index = WeightObject.GetWeightIndex(m_specialWeight, random);
            return m_specialEnemies[index];
        }
        private GameObject GetNormalSpawn()
        {
            var random = Random.Range(0f, 100f);
            var index = WeightObject.GetWeightIndex(m_weights, random);
            return m_spawnArray[index];
        }
    } 
}

