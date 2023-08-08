using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainActionShootAtTarget : BrainAction
    {
        [SerializeField] private EnemyProjectileWeapon m_weapon;
        
        public override void DoAction()
        {
            if (m_weapon == null) return;
            if (m_brain.Target == null) return;
            var shootDir = (m_brain.Target.position - m_brain.transform.parent.position).normalized; 
            m_weapon.SetShootingDirection(shootDir);
            m_weapon.WeaponStart();
        }
    }
}

