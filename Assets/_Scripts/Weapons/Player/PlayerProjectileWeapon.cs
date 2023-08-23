using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public enum ProjectileWeaponState
    {
        Idle,
        Start,
        Reload,
        RequestShoot,
        Shooting,
        DelayBetweenTwoShot,
        End,
    }
    public enum GunType
    {
        AUTO,
        SEMI_AUTO,
    }
    public class PlayerProjectileWeapon : Weapon
    {
        [SerializeField] private IntEvent m_magazineSizeEvent;
        public PlayerAim AimController;
        public ProjectileWeaponState CurrentState;
        [Header("Magazine")] 
        public GunType GunType;
        public float ReloadTime;
        public int Magazine;
        public float DelayBetweenTwoShot;
        //public CooldownClockUI CooldownClockUI;
        public ObjectPooler ObjectPooler;
        
        protected int m_curMagazine;
        protected float m_reloadTimer;
        protected float m_delayBetweenTwoShotTimer;
        protected PlayerHealth m_health;
        protected bool m_inputStop;

        public override void Initialize()
        {
            m_health = GetComponentInParent<PlayerHealth>();
            m_health.OnDeath += WeaponStop;
            m_curMagazine = Magazine;
            AimController.RotateCallback += RotateWeapon;
            transform.position += m_weaponOffsetPosition;
            ActivateWeapon(true);
            base.Initialize();
        }
        
        public override void ActivateWeapon(bool value)
        {
            base.ActivateWeapon(value);
            //GunSprite.gameObject.SetActive(m_isActive);
            if (!m_isActive)
            {
                WeaponStop();
            }
        }
        
        public override void SetOwner(Transform owner)
        {
            base.SetOwner(owner);
            // var controller = m_owner.GetComponent<PlayerController>();
            // controller.OnFlip += FlipWeapon;
        }
        protected override void FlipWeapon()
        {
            // var curPos = transform.localPosition;
            // curPos *= -1;
            // transform.localPosition = curPos;
            //Debug.Log("Flip weapon");
            base.FlipWeapon();
        }

        protected void RotateWeapon(Quaternion quaternion)
        {
            //transform.rotation = quaternion;
        }
        
        [ContextMenu("Start test")]
        public override void WeaponStart()
        {
            if (CurrentState != ProjectileWeaponState.Idle)
            {
                return;
            }
            CurrentState = ProjectileWeaponState.Start;
            base.WeaponStart();
        }

        protected override void Update()
        {
            if(!m_isActive) return;
            
            switch (CurrentState)
            {
                case ProjectileWeaponState.Idle:
                    CaseWeaponIdle();
                    break;
                case ProjectileWeaponState.Start:
                    CaseWeaponStart();
                    break;
                case ProjectileWeaponState.Reload:
                    CaseWeaponReload();
                    break;
                case ProjectileWeaponState.RequestShoot:
                    CaseWeaponShootRequest();
                    break;
                case ProjectileWeaponState.Shooting:
                    CaseWeaponShooting();
                    break;
                case ProjectileWeaponState.DelayBetweenTwoShot:
                    CaseWeaponDelayBetweenTwoShot();
                    break;
                case ProjectileWeaponState.End:
                    CaseWeaponEnd();
                    break;
            }
            base.Update();
        }

        protected virtual void CaseWeaponIdle()
        {
            
        }
        protected virtual void CaseWeaponStart()
        {
            if (CurrentState != ProjectileWeaponState.Start)
            {
                return;
            }
            m_inputStop = false;
            if (m_curMagazine <= 0)
            {
                m_reloadTimer = ReloadTime;
                CurrentState = ProjectileWeaponState.Reload;
                //CooldownClockUI.SetCooldownClock(m_reloadTimer, ReloadTime);
                //Debug.Log("Reload again?");
                return;
            }
            CurrentState = ProjectileWeaponState.RequestShoot;
            //Debug.Log("WeaponStart");
        }
        
        protected virtual void CaseWeaponReload()
        {
            m_reloadTimer -= Time.deltaTime;
            if (m_reloadTimer <= 0)
            {
                m_reloadTimer = 0;
                //Refill magazine;
                m_curMagazine = Magazine;
                m_magazineSizeEvent.Raise(m_curMagazine);
                //player still pressing and gun type is AUTO, we keep shooting
                if (GunType == GunType.AUTO)
                {
                    CurrentState = m_inputStop ? ProjectileWeaponState.End: ProjectileWeaponState.RequestShoot;
                }
                else if(GunType == GunType.SEMI_AUTO)
                {
                    CurrentState = ProjectileWeaponState.End;
                    //Debug.Log("End as semi auto");
                }
            }
            //CooldownClockUI.SetCooldownClock(m_reloadTimer, ReloadTime);
            //Debug.Log("WeaponReload");
        }
        protected virtual void CaseWeaponShootRequest()
        {
            //Not enough bullet
            if (m_curMagazine <= 0)
            {
                m_reloadTimer = ReloadTime;
                CurrentState = ProjectileWeaponState.Reload;
                //CooldownClockUI.SetCooldownClock(m_reloadTimer, ReloadTime);
                return;
            }

            if (GunType == GunType.AUTO && m_inputStop)
            {
                CurrentState = ProjectileWeaponState.End;
                return;
            }

            CurrentState = ProjectileWeaponState.Shooting;
            //Debug.Log("WeaponShootRequest");
        }
        protected virtual void CaseWeaponShooting()
        {
            if (GunType == GunType.SEMI_AUTO)
            {
                ShootProjectile();
                m_curMagazine--;
                m_magazineSizeEvent.Raise(m_curMagazine);
                if (m_curMagazine <= 0)
                {
                    CurrentState = ProjectileWeaponState.Reload;
                    m_reloadTimer = ReloadTime;
                    //CooldownClockUI.SetCooldownClock(m_reloadTimer, ReloadTime);
                }
                else
                {
                    CurrentState = ProjectileWeaponState.End;
                }
            }
            else if (GunType == GunType.AUTO)
            {
                ShootProjectile();
                m_curMagazine--;
                m_magazineSizeEvent.Raise(m_curMagazine);
                if (m_curMagazine <= 0)
                {
                    CurrentState = ProjectileWeaponState.Reload;
                    m_reloadTimer = ReloadTime;
                    //CooldownClockUI.SetCooldownClock(m_reloadTimer, ReloadTime);
                }
                else
                {
                    CurrentState = ProjectileWeaponState.DelayBetweenTwoShot;
                    m_delayBetweenTwoShotTimer = DelayBetweenTwoShot;
                }
            }
            //Debug.Log("WeaponShooting");
        }
        protected virtual void CaseWeaponDelayBetweenTwoShot()
        {
            m_delayBetweenTwoShotTimer -= Time.deltaTime;
            if (m_delayBetweenTwoShotTimer <= 0)
            {
                CurrentState = GunType == GunType.AUTO 
                    ? ProjectileWeaponState.RequestShoot
                    : ProjectileWeaponState.End;
            }
            //Debug.Log("WeaponDelayBetween2Shot");
        }
        protected virtual void CaseWeaponEnd()
        {
            //Debug.Log("WeaponEnd");
            m_inputStop = true;
            CurrentState = ProjectileWeaponState.Idle;
        }
        
        protected virtual void ShootProjectile()
        {
            var projectile = ObjectPooler.GetPooledGameObject().GetComponent<Projectile>();
            
            projectile.SpawnProjectile(transform.position, AimController.AimDirection);
        }
        
        public virtual void SetDirection(Vector2 newDirection)
        {
            //ShootingDirection = newDirection;
        }
        
        [ContextMenu("Stop test")]
        public override void WeaponStop()
        {
            m_inputStop = true;
            base.WeaponStop();
        }

        public override void ResetWeapon()
        {
            WeaponStop();
            CurrentState = ProjectileWeaponState.Idle;
            m_curMagazine = Magazine;
            base.ResetWeapon();
        }
    }
}

