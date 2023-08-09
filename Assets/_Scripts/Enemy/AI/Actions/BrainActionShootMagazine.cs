using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    /// <summary>
    /// Shoot within magazine of the weapon
    /// </summary>
    public class BrainActionShootMagazine : BrainAction
    {
        [SerializeField] private EnemyProjectileWeapon m_weapon;

        public override void DoAction()
        {
            if (m_weapon == null) return;
            m_weapon.WeaponStart();
        }
    }
}

