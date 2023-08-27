using JustGame.Scripts.Common;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class EnemyProjectileWeapon : Weapon
    {
        [SerializeField] protected ObjectPooler m_objectPooler;
        [SerializeField] protected float m_delayBetweenTwoShot;
        [SerializeField] protected PlaySoundFX m_shootSFX;
        
        protected Vector2 m_shootingDirection;
        protected EnemyHealth m_health;

        protected float m_delayBetweenTwoShotTimer;
        protected bool m_isCoolDown;
        
        public override void Initialize()
        {
            base.Initialize();
            m_health = GetComponentInParent<EnemyHealth>();
            m_health.OnDeath += WeaponStop;
            ActivateWeapon(true);
        }

        public override void WeaponStart()
        {
            base.WeaponStart();
            ShootProjectile();
        }

        public virtual void SetShootingDirection(Vector2 newDirection)
        {
            m_shootingDirection = newDirection;
        }
        
        protected virtual void ShootProjectile()
        {
            if (m_isCoolDown) return;
            var projectile = m_objectPooler.GetPooledGameObject().GetComponent<Projectile>();
            projectile.SpawnProjectile(transform.position,m_shootingDirection);
            m_isCoolDown = true;
            m_delayBetweenTwoShotTimer = m_delayBetweenTwoShot;

            if (m_shootSFX != null)
            {
                m_shootSFX.PlaySFX();
            }
        }

        protected override void Update()
        {
            base.Update();
            if (m_isCoolDown)
            {
                m_delayBetweenTwoShotTimer -= Time.deltaTime;
                if (m_delayBetweenTwoShotTimer <= 0)
                {
                    m_isCoolDown = false;
                }
            }
        }
    }
}

