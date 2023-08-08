
using System.Collections;
using JustGame.Scripts.Common;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class EnemyHealth : Health
    {
        [SerializeField] private AnimationParameter m_explodeAnim;
        
        protected override IEnumerator KillRoutine()
        {
            m_spriteRenderer.enabled = false;
            m_collider.enabled = false;
            m_explodeAnim.SetTrigger();
            yield return new WaitForSeconds(m_explodeAnim.Duration + m_delayBeforeDeath);
            gameObject.SetActive(false);
        }
    }
}

