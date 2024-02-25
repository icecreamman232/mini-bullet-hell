using JustGame.Scripts.Player;
using JustGame.Scripts.Weapons;
using UnityEngine;

namespace JustGame.Scripts.RuntimeSet
{
    /// <summary>
    /// Store runtime reference to common components
    /// for quick access without using GetComponent calls
    /// </summary>
    [CreateAssetMenu(menuName = "JustGame/Runtime Set/Player Component Set")]
    public class PlayerComponentSet : ScriptableObject
    {
        [SerializeField] private PlayerController m_playerController;
        [SerializeField] private PlayerHealth m_health;
        [SerializeField] private PlayerMovement m_movement;
        [SerializeField] private int m_playerLevel;
        
        public PlayerController Player => m_playerController;
        public PlayerHealth Health => m_health;
        public PlayerMovement Movement => m_movement;

        public int PlayerLevel => m_playerLevel;
        
        public void SetPlayer(PlayerController controller)
        {
            m_playerController = controller;
        }

        public void SetHealth(PlayerHealth health)
        {
            m_health = health;
        }

        public void SetMovement(PlayerMovement movement)
        {
            m_movement = movement;
        }

        public void SetPlayerLevel(int level)
        {
            m_playerLevel = level;
        }

        public void Reset()
        {
            Destroy(m_playerController.gameObject);
            m_playerController = null;
            m_health = null;
            m_movement = null;
            m_playerLevel = 0;
        }
    }
}

