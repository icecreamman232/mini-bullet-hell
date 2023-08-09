using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Spawn profile")]
    public class SpawnProfile : ScriptableObject
    {
        [SerializeField] private WeightObject[] m_weights;
        [SerializeField] private GameObject[] m_spawnArray;

        private void OnEnable()
        {
            WeightObject.ComputeSpawnArray(m_weights);
        }

        public GameObject GetNextSpawn()
        {
            var random = Random.Range(0f, 100f);
            var index = WeightObject.GetWeightIndex(m_weights, random);
            return m_spawnArray[index];
        }
    } 
}

