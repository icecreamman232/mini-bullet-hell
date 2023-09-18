using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_delayBeforeDestruction;
        [SerializeField] protected float m_maxDistanceTravel;
        [SerializeField] protected Transform m_bulletBody;
        [SerializeField] protected DamageHandler m_damageHandler;
        [SerializeField] protected AnimationParameter m_destroyAnim;
        [SerializeField] protected SpriteRenderer m_spriteRenderer;
        protected float m_initialSpeed;
        protected Vector2 m_moveDirection;
        protected float m_distanceTraveled;
        protected Vector2 m_originalPos;
        protected bool m_isDestroying;


        protected virtual void Awake()
        {
            m_initialSpeed = m_moveSpeed;
        }

        protected virtual void Start()
        {
            m_damageHandler.OnHit += DestroyBullet;
        }


        public virtual void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            m_isDestroying = false;
            m_moveDirection = direction;
            m_originalPos = position;
            transform.position = position;
            
            var angle = Mathf.Atan2(m_moveDirection.y, m_moveDirection.x) * Mathf.Rad2Deg + 90;
            m_bulletBody.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        }

        protected virtual void Movement()
        {
            if (m_isDestroying) return;
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
                DestroyBullet(null);
            }
        }
        
        public void DestroyBullet(GameObject instigator)
        {
            StartCoroutine(DestroyRoutine());
        }
        
        protected virtual IEnumerator DestroyRoutine()
        {
            if (m_isDestroying)
            {
                yield break;
            }

            m_moveDirection = Vector2.zero;
            m_isDestroying = true;
            yield return new WaitForSeconds(m_delayBeforeDestruction);
            
            if (m_destroyAnim != null)
            {
                m_destroyAnim.SetTrigger();
                yield return new WaitForSeconds(m_destroyAnim.Duration);
            }

            OnDisable();
        }

        protected virtual void OnDisable()
        {
            transform.Reset(ignoreScale:true);
            this.gameObject.SetActive(false);
        }
    }
}

