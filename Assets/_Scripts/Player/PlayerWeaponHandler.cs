using JustGame.Scripts.Managers;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerWeaponHandler : PlayerAbility
    {
        [SerializeField] protected Weapon m_curWeapon;

        public override void Initialize()
        {
            base.Initialize();
            m_curWeapon.Initialize();
            m_curWeapon.SetOwner(transform.parent);
        }
        
        protected override void HandleInput()
        {
            if (InputManager.Instance.GetLeftClickDown())
            {
                m_curWeapon.WeaponStart();
            }

            if (InputManager.Instance.GetLeftClickUp())
            {
                m_curWeapon.WeaponStop();
            }
            base.HandleInput();
        }

        public virtual void SetActivation(bool value)
        {
            m_curWeapon.ActivateWeapon(value);
        }
    }
}

