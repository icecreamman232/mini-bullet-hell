using JustGame.Scripts.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerWeaponHandler : PlayerAbility
    {
        [SerializeField] protected Weapon m_curWeapon;
        [SerializeField] protected Weapon m_secondWeapon;
        [SerializeField] protected Weapon m_thirdWeapon;
        [SerializeField] private DoubleShotPowerUp m_doubleShotPowerUp;
        [SerializeField] private TripleShotPowerUp m_tripleShotPowerUp;
        
        public override void Initialize()
        {
            base.Initialize();
            m_curWeapon.Initialize();
            m_curWeapon.SetOwner(transform.parent);

            m_doubleShotPowerUp.OnApplyPowerUp += TriggerDoubleShotPowerUp;
            m_tripleShotPowerUp.OnApplyPowerUp += TriggerTripleShotPowerUp;
        }
        
        protected override void HandleInput()
        {
            if (InputManager.Instance.GetLeftClickDown())
            {
                m_curWeapon.WeaponStart();
                if (m_doubleShotPowerUp.IsActive)
                {
                    m_secondWeapon.WeaponStart();
                }

                if (m_tripleShotPowerUp.IsActive)
                {
                    m_secondWeapon.WeaponStart();
                    m_thirdWeapon.WeaponStart();
                }
            }

            if (InputManager.Instance.GetLeftClickUp())
            {
                m_curWeapon.WeaponStop();
                if (m_doubleShotPowerUp.IsActive)
                {
                    m_secondWeapon.WeaponStop();
                }
                if (m_tripleShotPowerUp.IsActive)
                {
                    m_secondWeapon.WeaponStop();
                    m_thirdWeapon.WeaponStop();
                }
            }
            base.HandleInput();
        }

        public virtual void SetActivation(bool value)
        {
            m_curWeapon.ActivateWeapon(value);
        }

        private void TriggerDoubleShotPowerUp()
        {
            m_curWeapon.transform.localPosition = new Vector3(-0.3f, 0.3f, 0);
            
            m_secondWeapon.gameObject.SetActive(true);
            m_secondWeapon.transform.localPosition = new Vector3(0.3f, 0.3f, 0);
            m_doubleShotPowerUp.IsActive = true;
        }

        private void TriggerTripleShotPowerUp()
        {
            m_curWeapon.transform.localPosition = new Vector3(0, 0.6f ,0);
            
            m_secondWeapon.gameObject.SetActive(true);
            m_secondWeapon.transform.localPosition = new Vector3(0.3f, 0.3f, 0);
            
            m_thirdWeapon.gameObject.SetActive(true);
            m_thirdWeapon.transform.localPosition = new Vector3(-0.3f, 0.3f, 0);
            
            m_tripleShotPowerUp.IsActive = true;
        }
    }
}

