namespace JustGame.Scripts.Weapons
{
    public class RailPlayerProjectile : Projectile
    {
        protected override void Start()
        {
            ((RailDamageHandler)m_damageHandler).OnFinalHit += DestroyBullet;
        }

        protected override void OnDestroy()
        {
            ((RailDamageHandler)m_damageHandler).OnFinalHit -= DestroyBullet;
        }
    } 
}

