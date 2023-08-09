using JustGame.Scripts.Data;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private RuntimeWorldSet m_worldSet;
        [SerializeField] private SpawnProfile m_spawnProfile;
        private float m_timer;
        
        private void Update()
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_spawnProfile.DelayTimeBetweenTwoSpawn)
            {
                Instantiate(m_spawnProfile.GetNextSpawn(), m_worldSet.LevelBounds.GetRandomPoint(),
                    Quaternion.identity);
                m_timer = 0;
            }
        }
    }
}

