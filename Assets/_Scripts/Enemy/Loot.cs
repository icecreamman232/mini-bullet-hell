using System;
using JustGame.Scripts.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Enemy
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private float m_spawnRadius;
        [SerializeField] private GameObject m_lootObject;
        [SerializeField] private float m_minAmount;
        [SerializeField] private float m_maxAmount;
        
        public void SpawnLoot()
        {
            if (m_lootObject == null) return;
            var randAmount = Random.Range(m_minAmount, m_maxAmount + 1);
            for (int i = 0; i < randAmount; i++)
            {
                var spawnPos = Random.insideUnitCircle * m_spawnRadius + (Vector2)transform.position;
                var loot = Instantiate(m_lootObject, spawnPos, Quaternion.identity);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,m_spawnRadius);
        }
    }
}

