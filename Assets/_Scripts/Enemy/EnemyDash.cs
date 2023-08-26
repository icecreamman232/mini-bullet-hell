using System;
using System.Collections;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyDash : MonoBehaviour
    {
        [SerializeField] private float m_dashSpeed;
        [SerializeField] private float m_dashDistance;
        [SerializeField] private float m_minDistance;
        [SerializeField] private bool m_invulnerableWhileDashing;
        [SerializeField] private EnemyHealth m_Health;
        
        public Action OnFinishDash;
        private Coroutine m_dashCoroutine;

        private void Start()
        {
            m_Health.OnDeath += StopDash;
        }

        public void StartDash(Vector2 destination)
        {
            m_dashCoroutine = StartCoroutine(DashRoutine(destination));
        }

        private IEnumerator DashRoutine(Vector2 destination)
        {
            if (m_invulnerableWhileDashing)
            {
                m_Health.EnableDamageImmune();
            }
            while (CheckDistanceToDestination(destination))
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, 
                    destination, 
                    Time.deltaTime * m_dashSpeed / 10);
                
                yield return null;
            }
            
            if (m_invulnerableWhileDashing)
            {
                m_Health.DisableDamageImmune();
            }
            
            OnFinishDash?.Invoke();
        }

        public void StopDash()
        {
            if (m_dashCoroutine != null)
            {
                StopCoroutine(m_dashCoroutine);
            }
            
            if (m_invulnerableWhileDashing)
            {
                m_Health.DisableDamageImmune();
            }
            
            OnFinishDash?.Invoke();
        }
        
        private bool CheckDistanceToDestination(Vector2 destination)
        {
            var dist = Vector2.Distance(transform.position, destination);
            return dist > m_minDistance;
        }
    }
}

