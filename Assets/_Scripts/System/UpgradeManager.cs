using JustGame.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Managers
{
    public class UpgradeManager : Singleton<UpgradeManager>
    {
        [SerializeField] private UpgradeProfile[] m_componentProfiles;
        [SerializeField] private WeightObject[] m_weightObjects;

        private void Start()
        {
            WeightObject.ComputeSpawnArray(m_weightObjects);
        }

        public UpgradeProfile GetRandomProfile()
        {
            var random = Random.Range(0, 100f);
            var index = WeightObject.GetWeightIndex(m_weightObjects, random);
            return m_componentProfiles[index];
        }
    } 
}

