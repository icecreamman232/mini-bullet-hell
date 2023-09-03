
using System.Collections;
using JustGame.Scripts.Common;
using UnityEngine;

namespace  JustGame.Scripts.Weapons
{
    public class TriggerBomb : Projectile
    {
        [Header("Bomb")] 
        [SerializeField] private LayerMask m_targetPlayer;
        [SerializeField] private float m_detectRadius;
        [SerializeField] private float m_timeToSelfExplode;
        [SerializeField] private AnimationParameter m_explodeAnim;
        [SerializeField] private DamageHandler m_explosionArea;
        private bool m_isExploding;
        private float m_timer;
        private bool IsThereTargetInRange()
        {
            var target =Physics2D.OverlapCircle(transform.position, m_detectRadius,m_targetPlayer);
            if (target != null)
            {
                return true;
            }
            return false;
        }

        public override void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            base.SpawnProjectile(position, direction);
            m_timer = m_timeToSelfExplode;
        }

        protected override void Update()
        {
            base.Update();
            if (IsThereTargetInRange())
            {
                StartCoroutine(ExplodeRoutine());
            }

            if (m_timer > 0)
            {
                m_timer -= Time.deltaTime;
                if (m_timer <= 0)
                {
                    StartCoroutine(ExplodeRoutine());
                }
            }
            
        }

        protected IEnumerator ExplodeRoutine()
        {
            if (m_isExploding)
            {
                yield break;
            }
            m_isExploding = true;
            
            m_explodeAnim.SetTrigger();
            
            yield return new WaitForSeconds(m_explodeAnim.Duration);
            OnDisable();
            m_isExploding = false;

        }

        public void ActivateDamageArea()
        {
            m_explosionArea.gameObject.SetActive(true);
        }

        public void DeactivateDamageArea()
        {
            m_explosionArea.gameObject.SetActive(false);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,m_detectRadius);
        }
    }
}

