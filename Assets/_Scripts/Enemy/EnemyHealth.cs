
using System;
using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class EnemyHealth : Health
    {
        [SerializeField] private LevelDataSO m_levelData;
        [SerializeField] private AnimationParameter m_explodeAnim;
        
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
            yield return new WaitForSeconds(m_explodeAnim.Duration + m_delayBeforeDeath);
            Destroy(this.gameObject);
        }
    }
}

