using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_delayBeforeDestruction;
        [SerializeField] protected float m_maxDistanceTravel;
        [SerializeField] protected Transform m_bulletBody;
        protected Vector2 m_moveDirection;
        protected float m_distanceTraveled;
        protected Vector2 m_originalPos;
        
        public virtual void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            m_moveDirection = direction;
            m_originalPos = position;
            transform.position = position;
            
            var angle = Mathf.Atan2(m_moveDirection.y, m_moveDirection.x) * Mathf.Rad2Deg + 90;
            m_bulletBody.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        }

        protected virtual void Movement()
        {
            transform.Translate(m_moveDirection * ((m_moveSpeed / 10) * Time.deltaTime));
        }

        protected virtual void Update()
        {
            if (!gameObject.activeSelf) return;
            Movement();
            CheckTravelDistance();
        }

        private void CheckTravelDistance()
        {
            m_distanceTraveled = Vector2.Distance(m_originalPos, transform.position);
            if (m_distanceTraveled >= m_maxDistanceTravel)
            {
                Invoke("OnDestroy", m_delayBeforeDestruction);
            }
        }
        
        protected virtual void OnDestroy()
        {
            transform.Reset();
            this.gameObject.SetActive(false);
        }
    }
}

