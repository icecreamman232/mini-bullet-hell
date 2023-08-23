using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] protected bool m_isActive;
        [SerializeField] protected bool m_initialzeOnStart;
        [SerializeField] protected Transform m_owner;
        [SerializeField] protected Vector3 m_weaponOffsetPosition;
        
        protected virtual void Start()
        {
            if (m_initialzeOnStart)
            {
                Initialize();
            }
        }
        
        public virtual void Initialize()
        {
            
        }

        public virtual void ActivateWeapon(bool value)
        {
            m_isActive = value;
        }

        public virtual void SetOwner(Transform owner)
        {
            m_owner = owner;
        }

        protected virtual void FlipWeapon()
        {
            
        }
        
        public virtual void WeaponStart()
        {
            
        }

        public virtual void WeaponStop()
        {
            
        }

        public virtual void ResetWeapon()
        {
            
        }
        
        protected virtual void Update()
        {
            
        }
    }
}

