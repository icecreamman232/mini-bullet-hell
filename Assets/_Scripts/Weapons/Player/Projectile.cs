using System;
using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] protected float m_moveSpeed;
        [SerializeField] protected float m_delayBeforeDestruction;
        [SerializeField] protected float m_maxDistanceTravel;
        [SerializeField] protected ShipProfile m_shipProfile;
        [SerializeField] protected Transform m_bulletBody;
        [SerializeField] protected DamageHandler m_damageHandler;
        [SerializeField] protected AnimationParameter m_destroyAnim;
        [SerializeField] protected SpriteRenderer m_spriteRenderer;
        [Header("PowerUp")] 
        [SerializeField] protected EnableCriticalDamagePowerUp m_criticalDamagePowerUp;
        
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
            //if this is player projectile
            if (m_shipProfile != null)
            {
                CheckCriticalDamage();
            }
            
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

        protected virtual void CheckTravelDistance()
        {
            m_distanceTraveled = Vector2.Distance(m_originalPos, transform.position);
            if (m_distanceTraveled >= m_maxDistanceTravel)
            {
                DestroyBullet(null);
            }
        }
        
        protected void CheckCriticalDamage()
        {
            //Default critical damage
            var critChance = Random.Range(0f, 100f);
            if (critChance <= m_shipProfile.BaseCritChance)
            {
                m_damageHandler.SetDamageMultiplier(m_shipProfile.BaseCritDamageMultiplier);
            }

            //Critical damage by powerup
            if (m_criticalDamagePowerUp.IsActive)
            {
                var critChanceByPowerUp = Random.Range(0f, 100f);
                if (critChanceByPowerUp <= m_criticalDamagePowerUp.CritChance)
                {
                    var greatCritChance = Random.Range(0f, 100f);
                    if (greatCritChance < m_criticalDamagePowerUp.GreatCritChance)
                    {
                        m_damageHandler.SetDamageMultiplier(m_criticalDamagePowerUp.AverageCritMultiplier);
                    }
                    else
                    {
                        m_damageHandler.SetDamageMultiplier(m_criticalDamagePowerUp.GreatCritMultiplier);
                    }
                }
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

        protected virtual void OnDestroy()
        {
            m_damageHandler.OnHit -= DestroyBullet;
        }
    }
}

