using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Data;
using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class EnemyHealth : Health
    {
        [SerializeField] private LevelDataSO m_levelData;
        [SerializeField] private AnimationParameter m_explodeAnim;

        private Loot m_loot;

        protected override void Initialize()
        {
            base.Initialize();
            m_loot = GetComponent<Loot>();
        }

        private void OnEnable()
        {
            if (m_levelData.CurrentLvl <= 1) return;
            m_maxHealth *= (m_levelData.CurrentLvl * 102f / 100f);
        }

        protected override IEnumerator KillRoutine()
        {
            m_spriteRenderer.enabled = false;
            m_collider.enabled = false;
            m_explodeAnim.SetTrigger();
            yield return new WaitForSeconds(m_explodeAnim.Duration);
            if (m_loot != null)
            {
                m_loot.SpawnLoot();
            }
            Destroy(this.gameObject);
        }
    }
}

