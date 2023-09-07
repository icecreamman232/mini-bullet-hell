using JustGame.Scripts.Managers;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class DroneBreakBullet : MonoBehaviour
    {
        [SerializeField] private LayerMask m_toBreakMask;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_toBreakMask)) return;

            var projectile = other.gameObject.GetComponentInParent<Projectile>();
            BreakBullet(projectile);
        }

        private void BreakBullet(Projectile projectile)
        {
            projectile.DestroyBullet(this.gameObject);
        }
    }
}

