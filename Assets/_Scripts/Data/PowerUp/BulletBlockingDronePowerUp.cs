using JustGame.Scripts.Player;
using JustGame.Scripts.RuntimeSet;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/PowerUp/Iron Drone")]
    public class BulletBlockingDronePowerUp : PowerUpData
    {
        [SerializeField] private GameObject m_dronePrefab;
        [SerializeField] private Vector2 m_offsetSpawnPos;
        [SerializeField] private PlayerComponentSet m_playerComponentSet;
        public GameObject DronePrefab => m_dronePrefab;
        
        [ContextMenu("Trigger")]
        private void Test()
        {
            ApplyPowerUp();
        }

        public override void ApplyPowerUp()
        {
            base.ApplyPowerUp();
            IsActive = true;
            var drone = Instantiate(m_dronePrefab, m_playerComponentSet.Player.transform, worldPositionStays: true);
            var playerPos = m_playerComponentSet.Player.transform.position;
            drone.transform.position = (Vector2)playerPos + m_offsetSpawnPos;
            var movement = drone.GetComponent<DroneMovementFlyAround>();
            movement.SetOwner(m_playerComponentSet.Player.transform);
            
        }
    }
}

